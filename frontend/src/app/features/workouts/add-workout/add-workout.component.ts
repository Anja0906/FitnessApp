import { ExerciseTypeService } from './../services/exercise-type.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ExerciseTypeResponseDto } from '../models/exercise-type.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { WorkoutService } from '../services/workout.service';
import { WorkoutRequestDto, WorkoutResponseDto } from '../models/workout.model';

@Component({
  selector: 'app-add-workout',
  templateUrl: './add-workout.component.html',
  styleUrls: ['./add-workout.component.css'],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatOptionModule,
    CommonModule,
    ReactiveFormsModule,
  ],
})
export class AddWorkoutComponent implements OnInit {
  @Output() workoutAdded = new EventEmitter<WorkoutResponseDto>();
  exerciseTypeId = new FormControl(0, Validators.required);
  duration = new FormControl(0, [Validators.required, Validators.min(1)]);
  caloriesBurned = new FormControl(0, [Validators.required, Validators.min(1)]);
  intensity = new FormControl(0, [Validators.required, Validators.min(1), Validators.max(10)]);
  fatigueLevel = new FormControl(0, [Validators.required, Validators.min(0), Validators.max(10)]);
  dateTime = new FormControl(new Date(), Validators.required);
  notes = new FormControl('');
  userId!: number;

  constructor(private workoutService: WorkoutService, private exerciseTypeService:ExerciseTypeService) {}

  exerciseTypesList: ExerciseTypeResponseDto[] = [];

  ngOnInit(): void {
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.userId = parseInt(storedUserId, 10); 
    } else {
      console.error('Korisnički ID nije pronađen u localStorage!');
      return; 
    }
    this.exerciseTypeService.getExerciseTypes().subscribe({
      next: (response) => {
        this.exerciseTypesList = response;
      },
      error: (error) => {
        console.error('Error fetching types:', error);
      },
    });
  }

  isFormValid(): boolean {
    return (
      this.exerciseTypeId.valid &&
      this.duration.valid &&
      this.caloriesBurned.valid &&
      this.intensity.valid &&
      this.fatigueLevel.valid
    );
  }

  submitForm(): void {
    if (this.isFormValid()) {
      const workout: WorkoutRequestDto = {
        exerciseTypeId: this.exerciseTypeId.value || 0,
        duration: this.duration.value || 0,
        caloriesBurned: this.caloriesBurned.value || 0,
        intensity: this.intensity.value || 0,
        fatigueLevel: this.fatigueLevel.value || 0,
        notes: this.notes.value || '',
        userId: this.userId,
        dateTime: this.dateTime.value ? new Date(this.dateTime.value).toISOString() : new Date().toISOString(),

      };
      this.workoutService.addWorkout(workout).subscribe((addedWorkout) => {
        this.workoutAdded.emit(addedWorkout); 
        this.resetForm();
      });
    }
  }

  resetForm(): void {
    this.exerciseTypeId.reset(0);
    this.duration.reset(0);
    this.caloriesBurned.reset(0);
    this.intensity.reset(0);
    this.fatigueLevel.reset(0);
    this.notes.reset('');
    this.dateTime.reset(new Date());
  }
}
