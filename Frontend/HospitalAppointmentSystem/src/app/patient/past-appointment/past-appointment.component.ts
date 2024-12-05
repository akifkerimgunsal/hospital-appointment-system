import { Component, OnInit } from '@angular/core';
import { PatientService } from '../patient.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-past-appointment',
  templateUrl: './past-appointment.component.html',
  styleUrls: ['./past-appointment.component.scss'],
  standalone: true,
  imports: [CommonModule] // Gerekli modül burada eklendi
})
export class PastAppointmentComponent implements OnInit {
  pastAppointments: any[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.loadPastAppointments();
  }

  loadPastAppointments(): void {
    this.patientService.getPastAppointments().subscribe((appointments) => {
      this.pastAppointments = appointments;
    });
  }
}
