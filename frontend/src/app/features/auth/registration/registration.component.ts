import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCard } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  imports: [MatInputModule, MatFormFieldModule, MatButtonModule, MatCard, CommonModule, ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class RegistrationComponent {
  registrationForm!: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      username: ['', Validators.required],
      firstName: [''],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      hashedPassword: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      const userRequestDto = this.registrationForm.value;
      this.authService.register(userRequestDto).subscribe({
        next: () => {
          this.router.navigate(['/login'])
        },
        error: (err) => {
          console.error('Registration failed:', err);
        },
      });
    }
  }

  redirectToLogin():void{
    this.router.navigate(['/login'])
  }
}
