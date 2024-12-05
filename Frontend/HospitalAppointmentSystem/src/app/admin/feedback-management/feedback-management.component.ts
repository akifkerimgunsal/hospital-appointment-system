import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-feedback-management',
  templateUrl: './feedback-management.component.html',
  styleUrls: ['./feedback-management.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule] // Gerekli modüller burada eklendi
})
export class FeedbackManagementComponent implements OnInit {
  feedbacks: any[] = [];
  selectedFeedback: any = null;
  replyForm!: FormGroup;

  constructor(private adminService: AdminService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadFeedbacks();
    this.replyForm = this.fb.group({
      replyMessage: ['', Validators.required]
    });
  }

  loadFeedbacks(): void {
    this.adminService.getFeedbacks().subscribe((feedbacks) => {
      this.feedbacks = feedbacks;
    });
  }

  deleteFeedback(feedbackId: number): void {
    if (confirm('Bu geri bildirimi silmek istediğinize emin misiniz?')) {
      this.adminService.deleteFeedback(feedbackId).subscribe(() => {
        this.feedbacks = this.feedbacks.filter(feedback => feedback.id !== feedbackId);
        alert('Geri bildirim başarıyla silindi.');
      });
    }
  }

  replyToFeedback(feedback: any): void {
    this.selectedFeedback = feedback;
    this.replyForm.reset();
  }

  submitReply(): void {
    if (this.replyForm.invalid) {
      return;
    }

    const replyData = {
      ...this.selectedFeedback,
      replyMessage: this.replyForm.value.replyMessage,
      status: 'Tamamlandı'
    };

    this.adminService.replyToFeedback(replyData).subscribe(() => {
      alert('Geri bildirim başarıyla cevaplandı.');
      
      // Bildirim gönderme işlemi
      this.adminService.getUserContactInfo(replyData.userId).subscribe((contactInfo) => {
        const email = contactInfo.email;
        const phone = contactInfo.phone;
        
        // Bildirim göndermek
        this.adminService.sendUserNotification(replyData.userId).subscribe(() => {
          alert(`${email} adresine bildirime cevap verildiği bilgisi gönderildi`);
          alert(`${phone} telefon numarasına bildirime cevap verildiği bilgisi gönderildi`);
        });
      });

      this.loadFeedbacks();
      this.selectedFeedback = null;
    });
  }
}
