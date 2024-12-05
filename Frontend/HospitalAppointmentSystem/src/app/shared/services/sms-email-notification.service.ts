import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SmsEmailNotificationService {
  sendSmsNotification(phoneNumber: string, message: string): void {
    console.log(`[SMS Simülasyonu] SMS gönderildi: ${phoneNumber} - ${message}`);
  }

  sendEmailNotification(email: string, message: string): void {
    console.log(`[E-posta Simülasyonu] E-posta gönderildi: ${email} - ${message}`);
  }
}
