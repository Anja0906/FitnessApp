import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { WorkoutResponseDto } from '../models/workout.model';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-workout-detail',
  templateUrl: './workout-detail.component.html',
  styleUrls: ['./workout-detail.component.css'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule]
})
export class WorkoutDetailComponent {
  @Input() workout!: WorkoutResponseDto;
}
