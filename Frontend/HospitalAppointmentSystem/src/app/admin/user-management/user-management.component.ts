import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule]
})
export class UserManagementComponent implements OnInit {
  users: any[] = [];
  selectedUser: any = null;
  searchedUser: any = null; // Aranan kullanıcı
  identityNumber: string = ''; // Kimlik numarası
  editForm!: FormGroup;

  constructor(private adminService: AdminService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadUsers();
    this.editForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      birthDate: ['', Validators.required],  // Doğum Tarihi Eklendi
      role: ['', Validators.required]
    });
  }

  loadUsers(): void {
    this.adminService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }

  // Kimlik numarasına göre kullanıcıyı arama
  searchUserByIdentity(): void {
    this.adminService.getUserByIdentity(this.identityNumber).subscribe((user) => {
      this.searchedUser = user;
    });
  }

  deleteUser(userId: number): void {
    if (confirm('Bu kullanıcıyı silmek istediğinize emin misiniz?')) {
      this.adminService.deleteUser(userId).subscribe(() => {
        this.users = this.users.filter(user => user.id !== userId);
        alert('Kullanıcı başarıyla silindi.');
        this.searchedUser = null;
      });
    }
  }

  editUser(user: any): void {
    this.selectedUser = user;
    this.editForm.patchValue(user); // Formu seçilen kullanıcı ile doldur
  }

  saveUser(): void {
    if (this.editForm.invalid) {
      return;
    }

    const updatedUser = { ...this.selectedUser, ...this.editForm.value };
    this.adminService.updateUser(updatedUser).subscribe(() => {
      alert('Kullanıcı bilgileri başarıyla güncellendi.');
      this.loadUsers(); // Güncel kullanıcı listesini yükle
      this.selectedUser = null; // Modalı kapat
    });
  }
}
