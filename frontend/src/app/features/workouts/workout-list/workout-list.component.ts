import { Component, OnInit } from '@angular/core';
import { WorkoutDetailComponent } from '../workout-detail/workout-detail.component';
import { AddWorkoutComponent } from '../add-workout/add-workout.component';
import { CommonModule } from '@angular/common';
import { WorkoutResponseDto } from '../models/workout.model';
import { WorkoutService } from '../services/workout.service';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-workout-list',
  templateUrl: './workout-list.component.html',
  styleUrls: ['./workout-list.component.css'],
  imports: [WorkoutDetailComponent, AddWorkoutComponent, CommonModule, MatIcon],
})
export class WorkoutListComponent implements OnInit {
  workouts: WorkoutResponseDto[] = [];
  selectedWorkout: WorkoutResponseDto | null = null;
  userId!: number;
  currentPage: number = 1;
  itemsPerPage: number = 10;
  showAddWorkout: boolean = false;

  constructor(private workoutService: WorkoutService) {}

  ngOnInit(): void {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.userId = parseInt(storedUserId, 10);
    } else {
      console.error('Korisnički ID nije pronađen u localStorage!');
      return;
    }

    this.fetchWorkouts();
  }

  fetchWorkouts(): void {
    this.workoutService.getWorkouts(this.userId).subscribe({
      next: (response) => {
        this.workouts = response;
      },
      error: (error) => {
        console.error('Error fetching workouts:', error);
      },
    });
  }

  selectWorkout(workout: WorkoutResponseDto): void {
    this.selectedWorkout = workout;
    this.showAddWorkout = false;
  }

  toggleAddWorkout(): void {
    this.showAddWorkout = !this.showAddWorkout;
    this.selectedWorkout = null; 
  }

  onWorkoutAdded(newWorkout: WorkoutResponseDto): void {
    this.workoutService.getWorkouts(this.userId).subscribe({
      next: (response) => {
        this.workouts = response;
  
        const addedWorkout = this.workouts.find(w => w.id === newWorkout.id);
        this.selectedWorkout = addedWorkout || null;
  
        this.showAddWorkout = false;
      },
      error: (error) => {
        console.error('Error fetching workouts:', error);
      },
    });
  }
  

  get paginatedWorkouts(): WorkoutResponseDto[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    return this.workouts.slice(startIndex, endIndex);
  }

  nextPage(): void {
    if (this.currentPage * this.itemsPerPage < this.workouts.length) {
      this.currentPage++;
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}
