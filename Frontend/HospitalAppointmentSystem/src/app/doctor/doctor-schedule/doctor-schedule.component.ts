import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../doctor.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-schedule',
  templateUrl: './doctor-schedule.component.html',
  styleUrls: ['./doctor-schedule.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class DoctorScheduleComponent implements OnInit {
  schedule: any[] = [];
  selectedDay: any = null; // Seçilen gün
  selectedTimeSlots: any[] = []; // Seçilen saat aralıkları
  timeSlots: string[] = ['09:00 - 12:00', '13:30 - 17:00', '18:00 - 20:00']; // Örnek saat aralıkları

  constructor(private doctorService: DoctorService) {}

  ngOnInit(): void {
    this.loadSchedule();
  }

  loadSchedule(): void {
    this.doctorService.getSchedule().subscribe((schedule) => {
      this.schedule = schedule;
    });
  }

  selectDay(day: any): void {
    this.selectedDay = day;
    this.selectedTimeSlots = day.timeSlots || [];
  }

  toggleTimeSlot(timeSlot: string): void {
    const index = this.selectedTimeSlots.indexOf(timeSlot);
    if (index === -1) {
      this.selectedTimeSlots.push(timeSlot);
    } else {
      this.selectedTimeSlots.splice(index, 1);
    }
  }

  saveSchedule(): void {
    if (!this.selectedDay) return;

    this.selectedDay.timeSlots = this.selectedTimeSlots;
    this.doctorService.updateSchedule(this.selectedDay).subscribe(() => {
      alert('Çalışma takvimi başarıyla güncellendi.');

      // Doktora bildirim gönderme
      this.doctorService.getDoctorContactInfo(this.selectedDay.doctorId).subscribe((contactInfo) => {
        const email = contactInfo.email;
        const phone = contactInfo.phone;
        this.doctorService.sendDoctorNotification(this.selectedDay.doctorId).subscribe(() => {
          alert(`${email} adresine çalışma bilgileri gönderildi`);
          alert(`${phone} telefon numarasına çalışma bilgileri gönderildi`);
        });
      });

      this.loadSchedule(); // Güncel takvimi tekrar yükle
    });
  }
}
