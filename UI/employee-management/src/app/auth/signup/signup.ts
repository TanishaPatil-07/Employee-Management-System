import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth-service';
import { Router } from '@angular/router'
import { MatSnackBar } from '@angular/material/snack-bar';
import { MaterialModule } from '../../shared/material/material-module';
import { CommonModule } from '@angular/common';

type NewType = Router;

@Component({
  selector: 'app-signup',
  imports: [MaterialModule, FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css',
})
export class Signup {
  // fullname = '';
  Role = '';
  Email = '';
  // phoneno = '';
  // department = '';
  // designation = '';
  // salary = '';
  // dateofjoining = '';
  Password = '';

  message = '';
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  onSubmit() {
    if (
      !this.Role ||
      !this.Email ||
      // !this.phoneno ||
      // !this.department ||
      // !this.designation ||
      // !this.salary ||
      // !this.dateofjoining ||
      !this.Password
    ) {
      this.errorMessage = 'All fields are required.';
      this.snackBar.open(this.errorMessage, 'Close', { duration: 3000 });
      return;
    }

    const data = {
      // fullname: this.fullname,
      Role : this.Role,
      Email: this.Email,
      // phoneno: this.phoneno,
      // department: this.department,
      // designation: this.designation,
      // salary: this.salary,
      // dateofjoining: this.dateofjoining,
      Password: this.Password
    };
    this.authService.registerUser(data).subscribe({
      next: () => {
        this.message = 'Registration successful!';
        this.snackBar.open(this.message, 'Close', { duration: 1000 });
        setTimeout(() => this.router.navigate(['/login']), 1500);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Registration failed.';
        this.snackBar.open(this.errorMessage, 'Close', { duration: 2000 });
      }
    });
  }

  onClick() {
    this.router.navigate(['/login']);
  }
}