import { Component, Directive, OnInit } from '@angular/core';
import { EmployeeService } from '../employee-service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../../shared/material/material-module';

@Component({
  selector: 'app-view-employee',
  imports: [CommonModule, MaterialModule, FormsModule],
  templateUrl: './view-employee.html',
  styleUrl: './view-employee.css'
})
export class ViewEmployee implements OnInit {
  employeeId!: number;
  employee: any = null;
  message = '';

  constructor(private empService: EmployeeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadEmployee();
  }

  loadEmployee() {
    this.empService.getEmployeeById(this.employeeId).subscribe({
      next: (res) => {
        this.employee = {
          fullname: res.fullname ?? res.fullname ?? '',
          email: res.email ?? '',
          phone: res.phoneNo ?? res.phoneno ?? '',
          department: res.department ?? '',
          designation: res.designation ?? '',
          salary: res.salary ?? 0,
          dateOfJoining: res.dateOfJoining ?? res.dateofjoining ?? ''
        };
      },
      error: () => (this.message = 'Error loading employee details.')
    });
  }

  goBack() {
    this.router.navigate(['/employees']);
  }
}