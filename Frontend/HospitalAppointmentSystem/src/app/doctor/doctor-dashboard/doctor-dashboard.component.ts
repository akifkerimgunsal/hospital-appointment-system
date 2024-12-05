import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DoctorService } from '../doctor.service';

@Component({
  selector: 'app-doctor-dashboard',
  templateUrl: './doctor-dashboard.component.html',
  styleUrls: ['./doctor-dashboard.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class DoctorDashboardComponent implements OnInit {
  upcomingAppointments: any[] = [];
  nearestAppointment: any = null;
  reportText: string = '';
  attendanceStatus: 'arrived' | 'not_arrived' | null = null;

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.loadUpcomingAppointments();
  }

  loadUpcomingAppointments(): void {
    this.doctorService.getUpcomingAppointments().subscribe((appointments) => {
      this.upcomingAppointments = appointments;
      this.nearestAppointment = this.getNearestAppointment();
    });
  }

  getNearestAppointment(): any {
    const currentTime = new Date().getTime();
    return this.upcomingAppointments.reduce((nearest, appointment) => {
      const appointmentTime = new Date(appointment.date).getTime();
      return (Math.abs(appointmentTime - currentTime) < Math.abs(new Date(nearest.date).getTime() - currentTime))
        ? appointment
        : nearest;
    }, this.upcomingAppointments[0]);
  }

  markAttendance(status: 'arrived' | 'not_arrived'): void {
    this.attendanceStatus = status;
  }

  submitReportAndSave(): void {
    if (!this.attendanceStatus) {
      alert('Lütfen randevuya gelip gelmediğini işaretleyin.');
      return;
    }

    const appointmentId = this.nearestAppointment.id;

    // Önce randevu durumunu güncelle
    this.doctorService.updateAppointmentStatus(appointmentId, this.attendanceStatus).subscribe(() => {
      // Ardından rapor ekle
      if (this.reportText.trim()) {
        this.doctorService.submitMedicalReport(appointmentId, this.reportText).subscribe(() => {
          alert('Randevu durumu ve rapor başarıyla kaydedildi.');
          this.clearForm();
          this.loadUpcomingAppointments();
        });
      } else {
        alert('Randevu durumu başarıyla kaydedildi.');
        this.clearForm();
        this.loadUpcomingAppointments();
      }
    });
  }

  clearForm(): void {
    this.reportText = '';
    this.attendanceStatus = null;
  }
}
