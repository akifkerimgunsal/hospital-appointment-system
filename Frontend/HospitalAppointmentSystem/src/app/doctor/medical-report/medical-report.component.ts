import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../doctor.service';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-medical-report',
  templateUrl: './medical-report.component.html',
  styleUrls: ['./medical-report.component.scss'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})
export class MedicalReportComponent implements OnInit {
  patientReports: any[] = [];
  patientInfo: any = null;
  patientId: string = '';
  searchForm!: FormGroup;

  constructor(private doctorService: DoctorService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      identityNumber: ['', Validators.required]
    });
  }

  loadPatientReports(): void {
    if (!this.patientId) {
      alert('Lütfen bir kimlik numarası girin.');
      return;
    }

    // Kimlik numarasına göre hasta bilgilerini al (Doktor servisi üzerinden)
    this.doctorService.getPatientReports(this.patientId).subscribe((reports) => {
      if (reports && reports.length > 0) {
        const patient = reports[0].patient;
        this.patientInfo = {
          firstName: patient.firstName,
          lastName: patient.lastName,
          birthDate: patient.birthDate,
          phone: patient.phone
        };
        this.patientReports = reports;
      } else {
        this.patientInfo = null;
        this.patientReports = [];
      }
    });
  }

  viewReport(report: string): void {
    if (report) {
      alert(`Rapor: ${report}`);
    } else {
      alert('Bu randevu için rapor bulunmamaktadır.');
    }
  }
}
