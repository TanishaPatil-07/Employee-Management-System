import { Component } from '@angular/core';
import { AuthService } from '../auth-service';
import { Router } from '@angular/router';
import { MaterialModule } from '../../shared/material/material-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [MaterialModule, FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
credentials = { email: '', password: '' };

  constructor(private auth: AuthService, private router: Router) {}

  login() {
    this.auth.loginUser(this.credentials).subscribe({
      next: () => this.router.navigate(['/employees']),
      error: () => alert('Invalid email or password.')
    });
  }

    onClick() {
    this.router.navigate(['/login']);
  }
  onSignup(){
        this.router.navigate(['/signup']);

  }
}
