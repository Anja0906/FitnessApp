import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { TokenInterceptor } from './core/interceptor/token.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideHttpClient(withInterceptorsFromDi()), 
    {
          provide: HTTP_INTERCEPTORS,
          useClass: TokenInterceptor,
          multi: true,
        },
    provideRouter(routes), 
    provideAnimationsAsync(), 
    provideAnimationsAsync()]
};