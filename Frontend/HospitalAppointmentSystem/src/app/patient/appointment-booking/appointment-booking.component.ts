import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientService } from '../patient.service';
import { SmsEmailNotificationService } from '../../shared/services/sms-email-notification.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-appointment-booking',
  templateUrl: './appointment-booking.component.html',
  styleUrls: ['./appointment-booking.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class AppointmentBookingComponent implements OnInit {
  bookingForm: FormGroup;
  specialties: string[] = [];
  doctors: any[] = [];
  availableDates: any[] = [];
  availableTimes: string[] = [];
  appointmentBooked: boolean = false;
  email: string = ''; // Kullanıcının e-posta adresi
  phoneNumber: string = ''; // Kullanıcının telefon numarası

  constructor(private fb: FormBuilder, private patientService: PatientService, private notificationService: SmsEmailNotificationService) {
    this.bookingForm = this.fb.group({
      specialty: ['', Validators.required],
      doctorId: ['', Validators.required],
      appointmentDate: ['', Validators.required],
      appointmentTime: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadSpecialties();
    this.loadUserDetails(); // Kullanıcının e-posta ve telefon bilgilerini al
  }

  loadSpecialties(): void {
    this.patientService.getSpecialties().subscribe((specialties) => {
      this.specialties = specialties;
    });
  }

  loadDoctors(event: Event): void {
    const specialty = (event.target as HTMLSelectElement).value;
    this.patientService.getDoctorsBySpecialty(specialty).subscribe((doctors) => {
      this.doctors = doctors;
    });
  }

  loadAvailableDates(doctorId: string): void {
    this.patientService.getDoctorAvailableDates(doctorId).subscribe((dates) => {
      this.availableDates = dates;
    });
  }

  loadAvailableTimes(date: string): void {
    const doctorId = this.bookingForm.get('doctorId')?.value;
    this.patientService.getDoctorAvailableTimes(doctorId, date).subscribe((times) => {
      this.availableTimes = times;
    });
  }

  // Kullanıcı bilgilerini (e-posta ve telefon) çekme
  loadUserDetails(): void {
    this.patientService.getUserDetails().subscribe((user) => {
      this.email = user.email;
      this.phoneNumber = user.phoneNumber;
    });
  }

  bookAppointment(): void {
    if (this.bookingForm.valid) {
      this.patientService.bookAppointment(this.bookingForm.value).subscribe(() => {
        alert('Randevunuz başarıyla oluşturuldu.');
        this.appointmentBooked = true;

        // SMS ve e-posta bildirimlerini tetikleyin
        const message = 'Randevunuz başarıyla oluşturulmuştur.';

        this.patientService.sendSms(this.phoneNumber, message).subscribe(() => {
          console.log('SMS bildirimi başarıyla gönderildi.');
        });

        this.patientService.sendEmail(this.email, message).subscribe(() => {
          console.log('E-posta bildirimi başarıyla gönderildi.');
        });
      });
    }
  }
}
