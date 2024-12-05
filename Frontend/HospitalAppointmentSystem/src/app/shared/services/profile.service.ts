import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private apiUrl = 'http://localhost:4200/profile'; // Örnek API URL

  constructor(private http: HttpClient) {}

  // Profil bilgilerini alma
  getProfile(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  // Profil bilgilerini güncelleme
  updateProfile(profileData: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, profileData);
  }

  // Şifre değiştirme
  changePassword(passwordData: any): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/change-password`, passwordData);
  }
}
