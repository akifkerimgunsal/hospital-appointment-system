import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-schedule-management',
  templateUrl: './doctor-schedule-management.component.html',
  styleUrls: ['./doctor-schedule-management.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule]
})
export class DoctorScheduleManagementComponent implements OnInit {
  doctorSchedules: any[] = [];
  selectedSchedule: any = null;
  searchForm!: FormGroup;
  editForm!: FormGroup;

  constructor(private adminService: AdminService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      identityNumber: ['', Validators.required],
      workDate: ['', Validators.required]
    });

    this.editForm = this.fb.group({
      doctorName: [{value: '', disabled: true}, Validators.required], 
      workDate: ['', Validators.required],
    });
  }

  searchSchedule(): void {
    const identityNumber = this.searchForm.get('identityNumber')?.value;
    const workDate = this.searchForm.get('workDate')?.value;

    this.adminService.getDoctorScheduleByDate(identityNumber, workDate).subscribe((schedules) => {
      this.doctorSchedules = schedules;
    });
  }

  editSchedule(schedule: any): void {
    this.selectedSchedule = schedule;

    // 'attended' değerini boolean'a dönüştürüyoruz
    const attendedBoolean = (schedule.attended === 'true' || schedule.attended === true);

    this.editForm.patchValue({
      doctorName: schedule.doctorName,
      workDate: schedule.workDate,
      attended: attendedBoolean
    });
  }

  saveSchedule(): void {
    if (this.editForm.invalid) {
      return;
    }

    const updatedSchedule = { ...this.selectedSchedule, ...this.editForm.value };

    this.adminService.updateDoctorSchedule(updatedSchedule).subscribe(() => {
      alert('Çalışma bilgileri başarıyla güncellendi.');

      // Doktora email ve SMS ile bildirim gönderiliyor
      this.adminService.getDoctorContactInfo(updatedSchedule.doctorId).subscribe((contactInfo) => {
        const email = contactInfo.email;
        const phone = contactInfo.phone;
        this.adminService.sendDoctorNotification(updatedSchedule.doctorId).subscribe(() => {
          alert(`${email} adresine çalışma bilgileri gönderildi`);
          alert(`${phone} telefon numarasına çalışma bilgileri gönderildi`);
        });
      });

      this.searchSchedule();  // Güncel takvimi tekrar yükle
      this.selectedSchedule = null;
    });
  }
}
