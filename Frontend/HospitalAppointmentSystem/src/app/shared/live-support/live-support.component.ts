import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LiveSupportService } from '../../shared/services/live-support.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-live-support',
  templateUrl: './live-support.component.html',
  styleUrls: ['./live-support.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class LiveSupportComponent {
  isLiveChatAvailable = true;
  chatForm!: FormGroup;
  messages: { sender: string, text: string }[] = [];
  userId: string = '12345'; // Örnek kullanıcı ID'si

  constructor(private fb: FormBuilder, private liveSupportService: LiveSupportService) {}

  ngOnInit(): void {
    this.chatForm = this.fb.group({
      message: ['', Validators.required]
    });

    // Daha önce gönderilen mesajları al
    this.liveSupportService.getMessages(this.userId).subscribe((data) => {
      this.messages = data.map(msg => ({ sender: 'Kullanıcı', text: msg.message }));
    });
  }

  startLiveChat(): void {
    alert('Canlı destek simülasyonu başlatıldı! Bir temsilci simüle olarak sizinle irtibata geçecek.');
  }

  sendMessage(): void {
    const userMessage = this.chatForm.get('message')?.value;

    if (userMessage) {
      // Kullanıcının mesajını mesajlar listesine ekle
      this.messages.push({ sender: 'Kullanıcı', text: userMessage });

      // Simüle edilmiş yanıtı ekle
      this.messages.push({ sender: 'Canlı Destek', text: 'Sizi bir temsilcimize aktarıyorum, lütfen biraz bekleyiniz...' });

      // Mesajı veri tabanına kaydet
      this.liveSupportService.sendMessage(this.userId, userMessage).subscribe(() => {
        console.log('Mesaj veri tabanına kaydedildi:', userMessage);
      });

      // Formu sıfırla
      this.chatForm.reset();
    }
  }
}
