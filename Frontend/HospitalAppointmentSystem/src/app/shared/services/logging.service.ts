import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {
  private logApiUrl = 'http://localhost:4200/logs'; // Örnek API URL

  constructor(private http: HttpClient) {}

  log(message: string, module: string): void {
    console.log(`[${module} LOG] ${message}`);
    this.sendLogToServer({ message, module, type: 'LOG' });
  }

  logError(error: any, module: string): void {
    console.error(`[${module} ERROR]`, error);
    this.sendLogToServer({ error, module, type: 'ERROR' });
  }

  // Logları sunucuya gönder
  private sendLogToServer(log: { message?: string; error?: any; module: string; type: string }): void {
    this.http.post(this.logApiUrl, log).subscribe({
      next: () => console.log('Log başarıyla sunucuya gönderildi.'),
      error: () => console.error('Log sunucuya gönderilirken bir hata oluştu.'),
    });
  }
}
