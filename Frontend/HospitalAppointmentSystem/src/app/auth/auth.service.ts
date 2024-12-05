import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:4200/auth'; // API adresi örnek olarak verilmiştir.

  constructor(private http: HttpClient) {}

  // Kullanıcı giriş işlemi
  login(credentials: { emailOrPhone: string; password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      map((response: any) => {
        this.setSession(response.token, response.refreshToken, response.role);
        return response;
      })
    );
  }

  // Kullanıcı kayıt işlemi
  register(userData: {
    firstName: string;
    lastName: string;
    identityNumber: string;
    email: string;
    phone: string;
    password: string;
    isDoctor: boolean;
    specialty?: string;
  }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  // Kullanıcı çıkış işlemi
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('role');
  }

  // Kullanıcı rolünü getiren fonksiyon
  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  // Kullanıcının giriş yapıp yapmadığını kontrol eden fonksiyon
  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (token && !this.isTokenExpired(token)) {
      return true;
    }
    this.refreshToken().subscribe();
    return false;
  }

  // Token süresinin dolup dolmadığını kontrol eden fonksiyon
  isTokenExpired(token: string): boolean {
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return Math.floor((new Date).getTime() / 1000) >= expiry;
  }

  // Refresh token ile yeni access token alma işlemi
  refreshToken(): Observable<any> {
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) {
      return throwError('Refresh token not found');
    }
    return this.http.post(`${this.apiUrl}/refresh`, { refreshToken }).pipe(
      map((response: any) => {
        this.setSession(response.token, response.refreshToken, this.getUserRole() || '');
        return response;
      }),
      catchError((error: HttpErrorResponse) => {
        this.logout(); // Eğer refresh token da geçersizse çıkış yapıyoruz.
        return throwError(error);
      })
    );
  }

  // Token ve refresh token saklama işlemi
  private setSession(token: string, refreshToken: string, role: string) {
    localStorage.setItem('token', token);
    localStorage.setItem('refreshToken', refreshToken);
    localStorage.setItem('role', role);
  }
}
