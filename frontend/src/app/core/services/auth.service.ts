import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { LoginRequestDto, LoginResponseDto } from '../../features/auth/models/login.model';
import { UserRequestDto } from '../../features/user/models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = `${environment.apiUrl}`;
  private tokenKey = 'auth_token';
  private userIdKey = 'userId';
  private isLoggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());

  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(request: LoginRequestDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(`${this.apiUrl}/Auth/login`, request).pipe(
      tap((response) => {
        const token = response.token;
        localStorage.setItem(this.tokenKey, token);
        const decodedToken = this.decodeToken(token);
        const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        localStorage.setItem(this.userIdKey, userId);
        this.isLoggedInSubject.next(true);
      })
    );
  }

  register(request: UserRequestDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/User/register`, request);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedInSubject.next(false);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  getToken(): string | null {
    const token = localStorage.getItem(this.tokenKey);
    return token;
  }

  private decodeToken(token: string): any {
    const payload = token.split('.')[1]; 
    return JSON.parse(atob(payload));
  }
}
