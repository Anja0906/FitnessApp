import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserResponseDto } from '../models/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-profile',
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  user!: UserResponseDto;
  userId!: number;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.userId = parseInt(storedUserId, 10); 
    } else {
      console.error('Korisnički ID nije pronađen u localStorage!');
      return; 
    }

    this.userService.getUserProfile(this.userId).subscribe(
      (data) => {
        this.user = data;
      },
      (error) => {
        console.error('Greška pri dobavljanju korisničkih podataka:', error);
      }
    );
  }
}
