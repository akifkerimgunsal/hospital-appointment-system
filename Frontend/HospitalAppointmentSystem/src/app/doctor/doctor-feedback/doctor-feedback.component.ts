import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DoctorService } from '../doctor.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-feedback',
  templateUrl: './doctor-feedback.component.html',
  styleUrls: ['./doctor-feedback.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule] // Gerekli modüller burada eklendi
})
export class DoctorFeedbackComponent implements OnInit {
  feedbackForm: FormGroup;

  constructor(private fb: FormBuilder, private doctorService: DoctorService) {
    this.feedbackForm = this.fb.group({
      message: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  submitFeedback(): void {
    if (this.feedbackForm.valid) {
      this.doctorService.sendFeedback(this.feedbackForm.value).subscribe(() => {
        alert('Geri bildiriminiz başarıyla gönderildi.');
        this.feedbackForm.reset();
      });
    }
  }
}
