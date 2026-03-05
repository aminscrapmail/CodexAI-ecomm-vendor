import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginRequest, LoginResponse } from './models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private readonly http: HttpClient) {}

  login(payload: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, payload).pipe(
      tap((res) => {
        localStorage.setItem('auth_token', res.token);
        localStorage.setItem('username', res.username);
      })
    );
  }
}
