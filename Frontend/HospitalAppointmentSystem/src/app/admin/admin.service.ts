import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = 'http://localhost:4200/admin'; // Örnek API URL

  constructor(private http: HttpClient) {}

  // Kullanıcıları alma
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/users`);
  }

  // Kullanıcı kimlik numarasına göre alma
  getUserByIdentity(identityNumber: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/users?identityNumber=${identityNumber}`);
  }
  
  // Kullanıcı silme
  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/users/${userId}`);
  }

  // Kullanıcı güncelleme
  updateUser(userData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/users/${userData.id}`, userData);
  }

  // Kullanıcının iletişim bilgilerini alma
  getUserContactInfo(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/users/${userId}/contact`);
  }

  // Kullanıcıya bildirim gönderme
  sendUserNotification(userId: number): Observable<any> {
    return this.http.post(`/api/users/${userId}/send-notification`, {});
  }

  // Doktor çalışma takvimini alma
  getDoctorSchedule(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/doctor-appointments`);
  }

  // Doktor randevusu silme
  deleteDoctorSchedule(appointmentId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/doctor-appointments/${appointmentId}`);
  }

  // Doktor randevusu güncelleme
  updateDoctorSchedule(appointmentData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/doctor-appointments/${appointmentData.id}`, appointmentData);
  }

  // Doktor raporunu getirme
  getDoctorReport(appointmentId: number): Observable<string> {
    return this.http.get<string>(`${this.apiUrl}/doctor-appointments/${appointmentId}/report`);
  }

  // Doktor randevularını kimlik numarasına ve tarihe göre alma
  getDoctorScheduleByDate(identityNumber: string, date: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/doctor-appointments?identityNumber=${identityNumber}&date=${date}`);
  }

  // Hasta randevularını kimlik numarasına göre alma
  getPatientAppointmentsByIdentity(identityNumber: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/patient-appointments?identityNumber=${identityNumber}`);
  }

  // Hasta randevularını alma
  getPatientAppointments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/patient-appointments`);
  }

  // Hasta randevusu silme
  deletePatientAppointment(appointmentId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/patient-appointments/${appointmentId}`);
  }

  // Geri bildirimleri alma
  getFeedbacks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/feedbacks`);
  }

  // Geri bildirim silme
  deleteFeedback(feedbackId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/feedbacks/${feedbackId}`);
  }

  // Geri bildirime cevap verme
  replyToFeedback(replyData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/feedbacks/reply`, replyData);
  }

  // Sistem raporlarını alma
  getSystemReports(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/system-reports`);
  }

  // Sistem raporlarını indirme
  downloadSystemReports(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/system-reports/download`, { responseType: 'blob' });
  }

  // Hasta randevusu güncelleme
  updatePatientAppointment(appointmentData: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/patient-appointments/${appointmentData.id}`, appointmentData);
  }

  // Hasta raporunu getirme
  getPatientReport(appointmentId: number): Observable<string> {
    return this.http.get<string>(`${this.apiUrl}/patient-appointments/${appointmentId}/report`);
  }

  // Hastanın iletişim bilgilerini alma
  getPatientContactInfo(patientId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/patient/${patientId}/contact`);
  }

  // Doktorun iletişim bilgilerini alma
  getDoctorContactInfo(doctorId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/doctor/${doctorId}/contact`);
  }

  // Hastaya bildirim gönderme
  sendPatientNotification(patientId: number): Observable<any> {
    return this.http.post(`/api/patient/${patientId}/send-notification`, {});
  }

  // Doktora bildirim gönderme
  sendDoctorNotification(doctorId: number): Observable<any> {
    return this.http.post(`/api/doctor/${doctorId}/send-notification`, {});
  }
}
