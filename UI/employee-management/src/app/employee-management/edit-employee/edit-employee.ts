import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee-service';
import { ActivatedRoute, Router } from '@angular/router';
import { MaterialModule } from '../../shared/material/material-module';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-employee',
  imports: [FormsModule, CommonModule, MaterialModule],
  templateUrl: './edit-employee.html',
  styleUrl: './edit-employee.css',
})
export class EditEmployee implements OnInit {
employeeId!: number;
  employee: any = {
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

  constructor(private empService: EmployeeService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadEmployee();
  }

  loadEmployee() {
    this.empService.getEmployeeById(this.employeeId).subscribe({
      next: (res) => {
        // map backend fields if different names
        this.employee = {
          fullname: res.fullname ?? res.fullname ?? '',
          email: res.email ??  res.email ??'',
          phoneNo: res.phoneNo ?? res.phoneNo ?? '',
          department: res.department ?? '',
          designation: res.designation ?? '',
          salary: res.salary ?? 0,
          dateOfJoining: res.dateOfJoining ?? res.dateofjoining ?? ''
        };
      },
      error: () => (this.errorMessage = 'Error loading employee details.')
    });
  }

  onUpdate() {
    this.empService.updateEmployee(this.employeeId, this.employee).subscribe({
      next: () => {
        this.message = 'Employee updated successfully!';
        setTimeout(() => this.router.navigate(['/employees']), 1200);
      },
      error: () => {
        this.errorMessage = 'Error updating employee.';
      }
    });
  }
}