import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LiveSupportService {
  private apiUrl = 'http://localhost:4200/live-support'; // Örnek API URL

  constructor(private http: HttpClient) {}

  // Mesaj gönderme ve veri tabanına kaydetme
  sendMessage(userId: string, message: string): Observable<void> {
    const body = { userId, message };
    return this.http.post<void>(`${this.apiUrl}/messages`, body);
  }

  // Mesajları alma
  getMessages(userId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/messages?userId=${userId}`);
  }
}
