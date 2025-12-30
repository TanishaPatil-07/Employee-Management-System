import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';


interface LoginResponse {
  token: string;
 role : string;
fullname : string;
}

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private authState = new BehaviorSubject<boolean>(this.isLoggedIn());
  authState$ = this.authState.asObservable();

  private baseUrl = 'http://localhost:5119/api/Auth';

  constructor(private http: HttpClient, private router: Router) { }

  isBrowser(): boolean {
    return typeof window !== 'undefined' && typeof localStorage !== 'undefined';
  }


  registerUser(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signup`, data);
  }

  loginUser(credential: any): Observable<any> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, credential).pipe(
      tap((response) => {
        if (response && response.token && this.isBrowser()) {
          localStorage.setItem('token', response.token);
          localStorage.setItem('role', response.role);
          localStorage.setItem('username', response.fullname);
          this.authState.next(true);
        }
      })
    );
  }

  // logoutUser(): void {
  //   localStorage.clear();
  //   this.router.navigate(['/login']);
  // }
  logoutUser(): void {
    if (this.isBrowser()) {
      localStorage.clear();
    }
    this.router.navigate(['/login']);
  }

  // getToken(): string | null {
  //   return localStorage.getItem('token');
  // }

  getToken(): string | null {
    return this.isBrowser() ? localStorage.getItem('token') : null;
  }

  // getRole(): string | null {
  //   return localStorage.getItem('role');
  // }
  getRole(): string | null {
    return this.isBrowser() ? localStorage.getItem('role') : null;
  }

  // getUserName(): string | null {
  //   return localStorage.getItem('username');
  // }

  getUserName(): string | null {
    return this.isBrowser() ? localStorage.getItem('username') : null;
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
