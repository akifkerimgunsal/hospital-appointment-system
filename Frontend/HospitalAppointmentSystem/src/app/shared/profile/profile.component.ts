import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ProfileService } from '../services/profile.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule] // Gerekli modüller burada eklendi
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;

  constructor(private fb: FormBuilder, private profileService: ProfileService) {}

  ngOnInit(): void {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10,11}$/)]],
      password: [''],
      confirmPassword: [''],
    });

    this.loadProfile();
  }

  loadProfile(): void {
    this.profileService.getProfile().subscribe((data) => {
      this.profileForm.patchValue(data);
    });
  }

  saveProfile(): void {
    if (this.profileForm.invalid) {
      return;
    }

    this.profileService.updateProfile(this.profileForm.value).subscribe(() => {
      alert('Profil başarıyla güncellendi.');
    });
  }
}
