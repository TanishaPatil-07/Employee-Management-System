import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
//   get(id: number) {
//     throw new Error('Method not implemented.');
//   }
  private baseUrl = 'http://localhost:5119/api/Employee';

  constructor(private http: HttpClient) {}

  getAllEmployees(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}`);
  }

  getEmployeeById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  addEmployee(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, data);
  }

  updateEmployee(id: number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, data);
  }

  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
