import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private apiUrl = 'http://localhost:4200/doctor'; // Örnek API URL

  constructor(private http: HttpClient) {}

  // Doktorun çalışma takvimini getirme
  getSchedule(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/schedule`);
  }
  
  // Çalışma takvimini güncelleme (yeni eklenen saat aralıklarını kaydetme)
  updateSchedule(day: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/schedule/${day.id}`, day);
  }

  // Doktorun iletişim bilgilerini alma
  getDoctorContactInfo(doctorId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/contact/${doctorId}`);
  }

  // Doktora bildirim gönderme (çalışma takvimi değişiklikleri için)
  sendDoctorNotification(doctorId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/send-notification/${doctorId}`, {});
  }

  // Randevu durumu güncelleme (geldi/gelmedi)
  updateAppointmentStatus(appointmentId: number, status: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/appointments/${appointmentId}/status`, { status });
  }

  // Rapor ekleme
  submitMedicalReport(appointmentId: number, report: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/appointments/${appointmentId}/report`, { report });
  }

  // Geçmiş randevu bilgilerini ve raporlarını getirme
  getPatientReports(patientId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/patients/${patientId}/reports`);
  }

  // Yaklaşan randevuları getirme
  getUpcomingAppointments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/appointments/upcoming`);
  }

  // Doktorun geri bildirimini gönderme
  sendFeedback(feedback: { message: string }): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/feedback`, feedback);
  }
}
