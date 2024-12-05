import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-appointment-management',
  templateUrl: './patient-appointment-management.component.html',
  styleUrls: ['./patient-appointment-management.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]  // ReactiveFormsModule ve CommonModule eklendi
})
export class PatientAppointmentManagementComponent implements OnInit {
  patientAppointments: any[] = [];
  selectedAppointment: any = null;
  editForm!: FormGroup;
  searchForm!: FormGroup;
  today: Date = new Date();  // Bugünün tarihini ayarladık

  constructor(private adminService: AdminService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      identityNumber: ['', Validators.required]
    });

    this.editForm = this.fb.group({
      patientName: [{value: '', disabled: true}, Validators.required],
      doctorName: [{value: '', disabled: true}, Validators.required],
      date: ['', Validators.required],
      attended: ['', Validators.required]
    });
  }

  searchAppointments(): void {
    const identityNumber = this.searchForm.get('identityNumber')?.value;
    this.adminService.getPatientAppointmentsByIdentity(identityNumber).subscribe((appointments) => {
      this.patientAppointments = appointments.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()); // Tarihe göre sıralama
    });
  }

  editAppointment(appointment: any): void {
    this.selectedAppointment = appointment;
    this.editForm.patchValue(appointment);
  }

  saveAppointment(): void {
    if (this.editForm.invalid) {
      return;
    }

    const updatedAppointment = { ...this.selectedAppointment, ...this.editForm.value };
    this.adminService.updatePatientAppointment(updatedAppointment).subscribe(() => {
      alert('Randevu başarıyla güncellendi.');
      this.searchAppointments();  // Randevuları tekrar yükle
      this.selectedAppointment = null;

      // Bildirim gönderme
      this.adminService.getPatientContactInfo(updatedAppointment.patientId).subscribe((contactInfo) => {
        const email = contactInfo.email;
        const phone = contactInfo.phone;
        this.adminService.sendPatientNotification(updatedAppointment.patientId).subscribe(() => {
          alert(`${email} adresine randevu bilgileri gönderildi`);
          alert(`${phone} telefon numarasına randevu bilgileri gönderildi`);
        });
      });
    });
  }

  deleteAppointment(appointmentId: number): void {
    if (confirm('Bu randevuyu silmek istediğinize emin misiniz?')) {
      this.adminService.deletePatientAppointment(appointmentId).subscribe(() => {
        this.patientAppointments = this.patientAppointments.filter(app => app.id !== appointmentId);
        alert('Randevu başarıyla silindi.');
      });
    }
  }

  viewReport(appointmentId: number): void {
    this.adminService.getPatientReport(appointmentId).subscribe((report) => {
      alert(`Rapor: ${report}`);
    });
  }
}
