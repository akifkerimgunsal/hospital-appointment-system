import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientService } from '../patient.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-feedback',
  templateUrl: './patient-feedback.component.html',
  styleUrls: ['./patient-feedback.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule] // Gerekli modüller burada eklendi
})
export class PatientFeedbackComponent implements OnInit {
  feedbackForm: FormGroup;

  constructor(private fb: FormBuilder, private patientService: PatientService) {
    this.feedbackForm = this.fb.group({
      message: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  submitFeedback(): void {
    if (this.feedbackForm.valid) {
      this.patientService.sendFeedback(this.feedbackForm.value).subscribe(() => {
        alert('Geri bildiriminiz başarıyla gönderildi.');
        this.feedbackForm.reset();
      });
    }
  }
}
