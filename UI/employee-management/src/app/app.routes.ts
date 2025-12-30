import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Signup } from './auth/signup/signup';
import { authGuardGuard } from './auth/auth-guard-guard';
import { EmployeeList } from './employee-management/employee-list/employee-list';
import { ViewEmployee } from './employee-management/view-employee/view-employee';
import { AddEmployee } from './employee-management/add-employee/add-employee';
import { EditEmployee } from './employee-management/edit-employee/edit-employee';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'signup', component: Signup },
  {
    path: 'employees',
    component: EmployeeList,
    canActivate: [authGuardGuard],
  },
  {
    path: 'employees/:id', 
    component: ViewEmployee,
    canActivate: [authGuardGuard],
  },
    {
    path: 'addemployee', 
    component: AddEmployee,
    canActivate: [authGuardGuard],
  },
    {
    path: 'editemployee/:id', 
    component: EditEmployee
    // canActivate: [authGuardGuard],
  },
//   { path: '**', redirectTo: 'login' },
];
