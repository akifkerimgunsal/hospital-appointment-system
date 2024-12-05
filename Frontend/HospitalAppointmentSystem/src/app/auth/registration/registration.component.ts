import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule]
})
export class RegistrationComponent implements OnInit {
  registrationForm!: FormGroup;
  errorMessage: string = '';
  isDoctor: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      identityNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      birthDate: ['', Validators.required], // Doğum tarihi eklendi
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10,11}$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
      isDoctor: [false],
      specialty: ['']
    }, { validator: this.passwordMatchValidator });
    
    this.registrationForm.get('isDoctor')?.valueChanges.subscribe((isDoctor) => {
      this.isDoctor = isDoctor;
      const specialtyControl = this.registrationForm.get('specialty');
      if (isDoctor) {
        specialtyControl?.setValidators(Validators.required);
      } else {
        specialtyControl?.clearValidators();
      }
      specialtyControl?.updateValueAndValidity();
    });
  }

  passwordMatchValidator(formGroup: FormGroup) {
    return formGroup.get('password')?.value === formGroup.get('confirmPassword')?.value
      ? null
      : { mismatch: true };
  }

  onSubmit(): void {
    if (this.registrationForm.invalid) {
      return;
    }

    this.authService.register(this.registrationForm.value).subscribe({
      next: () => this.router.navigate(['/login']),
      error: (err) => {
        this.errorMessage = err.error.message || 'Registration failed. Please try again.';
      }
    });
  }
}
