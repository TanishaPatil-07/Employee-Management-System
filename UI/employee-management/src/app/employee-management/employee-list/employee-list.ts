import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee-service';
import { AuthService } from '../../auth/auth-service';
import { Router, RouterLink } from '@angular/router';
import { MaterialModule } from '../../shared/material/material-module';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-list',
  imports: [MaterialModule, FormsModule, CommonModule,RouterLink],
  templateUrl: './employee-list.html',
  styleUrls: ['./employee-list.css'],
})
export class EmployeeList implements OnInit {
  employees: any[] = [];
  filteredEmployees: any[] = [];
  searchTerm = '';
  isLoggedIn = false;
  role: string | null = null;
  loading = true;
  message = '';

  constructor(
    private empService: EmployeeService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadAllEmployees();
    this.isLoggedIn = this.authService.isLoggedIn();
    this.role = this.authService.getRole();
  }

  loadAllEmployees() {
    this.loading = true;
    this.empService.getAllEmployees().subscribe({
      next: (res) => {
        this.employees = res || [];
        this.filteredEmployees = this.employees;
        this.loading = false;
      },
      error: () => {
        this.message = 'Error loading employees.';
        this.loading = false;
      },
    });
  }

  searchEmployees() {
    if (!this.searchTerm.trim()) {
      this.filteredEmployees = this.employees;
      return;
    }
    const s = this.searchTerm.toLowerCase();
    this.filteredEmployees = this.employees.filter((e) =>
      (e.fullname || '').toLowerCase().includes(s) ||
      (e.email || '').toLowerCase().includes(s) ||
      (e.department || '').toLowerCase().includes(s) ||
      (e.designation || '').toLowerCase().includes(s) ||
      (e.phoneNo || '').toLowerCase().includes(s)
    );
  }

  deleteEmployee(id: number) {
    if (!confirm('Are you sure you want to delete this employee?')) return;
    this.empService.deleteEmployee(id).subscribe({
      next: () => {
        this.filteredEmployees = this.filteredEmployees.filter(e => e.id !== id);
        this.employees = this.employees.filter(e => e.id !== id);
        this.message = 'Employee deleted successfully.';
      },
      error: () => {
        this.message = 'Error deleting employee.';
      },
    });
  }

  viewEmployee(id: number): void {
    this.router.navigate(['/employees', id]);
  }

  editEmployee(id:number) {
    this.router.navigate(['/editemployee', id]);
  }

  goToAddEmployee() {
    this.router.navigate(['/addemployee']);
  }

   isAdmin(): boolean {
    return this.role === 'Admin';
  }

  // isSubAdmin(): boolean {
  //   return this.role === 'SubAdmin';
  // }
}
