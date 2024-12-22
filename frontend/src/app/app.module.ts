import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { AuthService } from './core/services/auth.service';
import { TokenInterceptor } from './core/interceptor/token.interceptor';
import { MaterialCustomModuleModel } from './material-custom-module/material-custom-module';
import { AuthModule } from './features/auth/auth.module';
import { ProfileModule } from './features/user/profile/profile.module';
import { ProgressModule } from './features/progress/progress.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes), 
    MaterialCustomModuleModel,
    AuthModule,
    ProfileModule,
    ProgressModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  bootstrap: [AppComponent],
})
export class AppModule {}
