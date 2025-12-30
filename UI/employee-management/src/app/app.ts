import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthModule } from './auth/auth-module';
import { SharedModule } from './shared/shared-module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,
    CommonModule,
    AuthModule,
    SharedModule,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('employee-management');
}
