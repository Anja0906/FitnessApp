import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        CommonModule,
        LoginComponent, 
        RegistrationComponent,
        RouterModule.forChild([
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegistrationComponent },
          ]),
      ],
    exports: [LoginComponent, RegistrationComponent],
  })
  export class AuthModule {}
