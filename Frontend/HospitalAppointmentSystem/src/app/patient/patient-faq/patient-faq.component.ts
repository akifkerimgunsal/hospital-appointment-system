import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-faq',
  templateUrl: './patient-faq.component.html',
  styleUrls: ['./patient-faq.component.scss'],
  standalone: true,
  imports: [CommonModule] // Gerekli modül burada eklendi
})
export class FaqComponent {
  faqs = [
    {
      question: 'Nasıl randevu alabilirim?',
      answer: 'Randevu almak için ana panelde "Randevu Al" butonuna tıklayın ve istediğiniz doktoru ve tarihi seçin.'
    },
    {
      question: 'Randevumu nasıl iptal ederim?',
      answer: 'Randevularınız kısmına giderek ilgili randevuyu iptal edebilirsiniz.'
    },
    {
      question: 'Doktor raporlarına nasıl ulaşabilirim?',
      answer: 'Geçmiş randevular kısmından doktor raporlarını görüntüleyebilirsiniz.'
    }
  ];
}
