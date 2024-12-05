import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule]  // HttpClientModule eklendi
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      emailOrPhone: ['', [Validators.required, Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$|^[0-9]{10,11}$/)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.authService.login(this.loginForm.value).subscribe({
      next: () => {
        const userRole = this.authService.getUserRole();
        if (userRole === 'Admin') {
          this.router.navigate(['/admin-dashboard']);
        } else if (userRole === 'Doctor') {
          this.router.navigate(['/doctor-dashboard']);
        } else {
          this.router.navigate(['/patient-dashboard']);
        }
      },
      error: (err) => {
        this.errorMessage = err.error.message || 'Login failed. Please try again.';
      },
    });
  }
}
