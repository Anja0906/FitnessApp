import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    console.log('Intercepting request:', request.url); // Debug log

    if (token) {
      console.log('Adding Authorization header:', token); // Debug log
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      });
    } else {
      console.log('No token found, skipping Authorization header'); // Debug log
    }

    return next.handle(request);
  }
}
