import { Component, OnInit } from '@angular/core';
import { PatientService } from '../patient.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-dashboard',
  templateUrl: './patient-dashboard.component.html',
  styleUrls: ['./patient-dashboard.component.scss'],
  standalone: true,
  imports: [CommonModule] // Gerekli modül burada eklendi
})
export class PatientDashboardComponent implements OnInit {
  upcomingAppointments: any[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.loadUpcomingAppointments();
  }

  loadUpcomingAppointments(): void {
    this.patientService.getUpcomingAppointments().subscribe((appointments) => {
      this.upcomingAppointments = appointments;
    });
  }
}
