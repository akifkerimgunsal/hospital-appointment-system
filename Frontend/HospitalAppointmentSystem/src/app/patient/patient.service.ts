import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'http://localhost:4200'; // Örnek API URL

  constructor(private http: HttpClient) {}

  // Kullanıcı detaylarını (e-posta ve telefon) getirme
  getUserDetails(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/user-details`);
  }

  // Uzmanlık alanlarını getirme
  getSpecialties(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/specialties`);
  }

  // Uzmanlık alanına göre doktorları getirme
  getDoctorsBySpecialty(specialty: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/doctors?specialty=${specialty}`);
  }

  // Randevu alma
  bookAppointment(appointmentData: any): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/appointments`, appointmentData);
  }

  // Geçmiş randevuları getirme
  getPastAppointments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/appointments/past`);
  }

  // Yaklaşan randevuları getirme
  getUpcomingAppointments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/appointments/upcoming`);
  }

  // Geri bildirim gönderme
  sendFeedback(feedbackData: any): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/feedback`, feedbackData);
  }

  // SMS gönderme servisi
  sendSms(phoneNumber: string, message: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/notifications/sms`, { phoneNumber, message });
  }

  // E-posta gönderme servisi
  sendEmail(email: string, message: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/notifications/email`, { email, message });
  }

  // Doktorun uygun günlerini getirme
  getDoctorAvailableDates(doctorId: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/doctors/${doctorId}/available-dates`);
  }

  // Doktorun uygun saat aralıklarını getirme
  getDoctorAvailableTimes(doctorId: string, date: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/doctors/${doctorId}/available-times?date=${date}`);
  }
}
