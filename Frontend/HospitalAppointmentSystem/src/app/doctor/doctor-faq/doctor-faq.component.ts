import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-doctor-faq',
  standalone: true,
  templateUrl: './doctor-faq.component.html',
  styleUrls: ['./doctor-faq.component.scss'],
  imports: [CommonModule]
})
export class DoctorFaqComponent implements OnInit {

  faqs: { question: string, answer: string }[] = [];

  ngOnInit(): void {
    this.loadFaqs();
  }

  loadFaqs(): void {
    // Sıkça sorulan sorular burada sabit olarak tanımlandı, gelecekte bir API'den çekilebilir
    this.faqs = [
      {
        question: 'Sisteme giriş yapamıyorum, ne yapmalıyım?',
        answer: 'Lütfen şifrenizi doğru girdiğinizden emin olun. Şifreyi sıfırlamak için "Şifremi Unuttum" bağlantısına tıklayabilirsiniz.'
      },
      {
        question: 'Randevu iptal etmek istiyorum, nasıl yaparım?',
        answer: 'Randevularınızı Randevu Yönetimi bölümünden iptal edebilirsiniz.'
      },
      {
        question: 'Çalışma saatlerimi nasıl düzenlerim?',
        answer: 'Çalışma saatlerinizi "Çalışma Takvimi" sayfasından seçebilir ve düzenleyebilirsiniz.'
      },
      {
        question: 'Hastaya nasıl rapor ekleyebilirim?',
        answer: 'Rapor eklemek istediğiniz randevuyu seçerek ilgili rapor alanına tıklayıp raporunuzu girebilirsiniz.'
      }
    ];
  }
}
