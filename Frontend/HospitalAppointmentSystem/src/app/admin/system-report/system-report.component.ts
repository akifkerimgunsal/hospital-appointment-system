import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-system-report',
  templateUrl: './system-report.component.html',
  styleUrls: ['./system-report.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class SystemReportComponent implements OnInit {
  totalUsers: number = 0;
  totalAppointments: number = 0;
  totalDoctors: number = 0;  // Yeni eklenen alan
  totalPatients: number = 0; // Yeni eklenen alan

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.adminService.getSystemReports().subscribe((reports) => {
      this.totalUsers = reports.totalUsers;
      this.totalAppointments = reports.totalAppointments;
      this.totalDoctors = reports.totalDoctors;  // Yeni eklenen alan
      this.totalPatients = reports.totalPatients; // Yeni eklenen alan
    });
  }

  downloadReports(): void {
    this.adminService.downloadSystemReports().subscribe((blob) => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'system-reports.csv';
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
