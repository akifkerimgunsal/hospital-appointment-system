import { Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

// Bileşenler
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { DoctorDashboardComponent } from './doctor/doctor-dashboard/doctor-dashboard.component';
import { PatientDashboardComponent } from './patient/patient-dashboard/patient-dashboard.component';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { ProfileComponent } from './shared/profile/profile.component';  // Ortak profil bileşeni

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./auth/registration/registration.component').then(m => m.RegistrationComponent) },
  { path: 'admin-dashboard', loadComponent: () => import('./admin/admin-dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)},
  { path: 'doctor-dashboard', loadComponent: () => import('./doctor/doctor-dashboard/doctor-dashboard.component').then(m => m.DoctorDashboardComponent)},
  { path: 'patient-dashboard', loadComponent: () => import('./patient/patient-dashboard/patient-dashboard.component').then(m => m.PatientDashboardComponent)},
  { path: 'profile', loadComponent: () => import('./shared/profile/profile.component').then(m => m.ProfileComponent)}, 
  { path: '**', redirectTo: '/login' }  // Tanımlanmayan rotalar login'e gider
];
