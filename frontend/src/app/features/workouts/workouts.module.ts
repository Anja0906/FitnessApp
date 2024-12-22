import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { WorkoutsRoutingModule } from './workouts-routing.module';
import { WorkoutListComponent } from './workout-list/workout-list.component';
import { WorkoutDetailComponent } from './workout-detail/workout-detail.component';
import { AddWorkoutComponent } from './add-workout/add-workout.component';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: WorkoutListComponent },
    ]),
    AddWorkoutComponent, 
    WorkoutDetailComponent,
    WorkoutListComponent,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
  ],
})
export class WorkoutsModule {}
