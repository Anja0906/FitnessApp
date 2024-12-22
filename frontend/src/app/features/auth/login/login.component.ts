import { CommonModule } from '@angular/common';
import { FormsModule, FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import {ChangeDetectionStrategy, Component, CUSTOM_ELEMENTS_SCHEMA, effect, signal} from '@angular/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [MatFormFieldModule, MatButtonModule, MatInputModule, FormsModule, ReactiveFormsModule, CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LoginComponent {
  loginForm!: FormGroup;
  requestFail = false;

  constructor(private fb: FormBuilder, private authService:AuthService, private router: Router) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  submit() {
    if (this.loginForm.valid) {
      const loginData = this.loginForm.value;
      console.log('Login data:', loginData);
  
      this.authService.login(loginData).subscribe({
        next: (response) => {
          console.log('Login successful, token:', response.token);
          this.router.navigate(['/home']);
        },
        error: (err) => {
          console.error('Login failed', err);
          this.requestFail = true;
        },
      });
    } else {
      this.requestFail = false;
    }
  }

  redirectToRegistration():void{
    this.router.navigate(['/register']);
  }

  errorMessage(): string | null {
    if (this.loginForm.get('username')?.hasError('required')) {
      return 'Email is required';
    } else if (this.loginForm.get('username')?.hasError('email')) {
      return 'Invalid email address';
    } else if (this.loginForm.get('password')?.hasError('required')) {
      return 'Password is required';
    }else if (this.requestFail) {
      return 'Wrong username or password!';
    }
    return null;
  }
  
}

