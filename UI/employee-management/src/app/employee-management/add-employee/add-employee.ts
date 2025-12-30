import { Component } from '@angular/core';
import { EmployeeService } from '../employee-service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../shared/material/material-module';

@Component({
  selector: 'app-add-employee',
  imports: [FormsModule, CommonModule, MaterialModule],
  templateUrl: './add-employee.html',
  styleUrl: './add-employee.css',
})
export class AddEmployee {
employee = {
    fullname: '',
    email: '',
    phoneNo: '',
    department: '',
    designation: '',
    salary: 0,
    dateOfJoining: ''
  };
  message = '';
  errorMessage = '';

  constructor(private empService: EmployeeService, private router: Router) {}

  onSubmit() {
    if (!this.employee.fullname || !this.employee.email || !this.employee.phoneNo) {
      this.errorMessage = 'Name, Email and Phone are required.';
      return;
    }

    this.empService.addEmployee(this.employee).subscribe({
      next: () => {
        this.message = 'Employee added successfully!';
        setTimeout(() => this.router.navigate(['/employees']), 1200);
      },
      error: () => {
        this.errorMessage = 'Error adding employee.';
      }
    });
  }
}