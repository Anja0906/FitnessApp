import { TestBed, ComponentFixture } from '@angular/core/testing';
import { AddWorkoutComponent } from './add-workout.component';
import { WorkoutService } from '../services/workout.service';
import { ExerciseTypeService } from '../services/exercise-type.service';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { of } from 'rxjs';

describe('AddWorkoutComponent', () => {
  let component: AddWorkoutComponent;
  let fixture: ComponentFixture<AddWorkoutComponent>;
  let mockWorkoutService: jasmine.SpyObj<WorkoutService>;
  let mockExerciseTypeService: jasmine.SpyObj<ExerciseTypeService>;

  beforeEach(async () => {
    mockWorkoutService = jasmine.createSpyObj('WorkoutService', ['addWorkout']);
    mockExerciseTypeService = jasmine.createSpyObj('ExerciseTypeService', ['getExerciseTypes']);
    mockExerciseTypeService.getExerciseTypes.and.returnValue(
      of([{ id: 1, name: 'Running', description: 'Cardio exercise' }])
    );

    await TestBed.configureTestingModule({
      imports: [
        AddWorkoutComponent,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatOptionModule,
        CommonModule,
      ],
      providers: [
        { provide: WorkoutService, useValue: mockWorkoutService },
        { provide: ExerciseTypeService, useValue: mockExerciseTypeService },
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    spyOn(localStorage, 'getItem').and.callFake((key: string) => {
      if (key === 'userId') {
        return '1';
      }
      return null;
    });

    fixture = TestBed.createComponent(AddWorkoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch exercise types on init', () => {
    expect(mockExerciseTypeService.getExerciseTypes).toHaveBeenCalled();
    expect(component.exerciseTypesList.length).toBe(1);
  });

  it('should validate form correctly', () => {
    component.duration.setValue(30);
    component.caloriesBurned.setValue(200);
    component.intensity.setValue(5);
    component.fatigueLevel.setValue(3);
    component.exerciseTypeId.setValue(1);

    expect(component.isFormValid()).toBeTrue();
  });

  it('should call addWorkout on form submit', () => {
    mockWorkoutService.addWorkout.and.returnValue(of({ id: 1 } as any));

    component.duration.setValue(30);
    component.caloriesBurned.setValue(200);
    component.intensity.setValue(5);
    component.fatigueLevel.setValue(3);
    component.exerciseTypeId.setValue(1);
    component.submitForm();

    expect(mockWorkoutService.addWorkout).toHaveBeenCalled();
  });

  it('should reset the form after submission', () => {
    mockWorkoutService.addWorkout.and.returnValue(of({ id: 1 } as any));

    component.duration.setValue(30);
    component.caloriesBurned.setValue(200);
    component.intensity.setValue(5);
    component.fatigueLevel.setValue(3);
    component.exerciseTypeId.setValue(1);
    component.submitForm();

    expect(component.duration.value).toBe(0);
    expect(component.caloriesBurned.value).toBe(0);
    expect(component.intensity.value).toBe(0);
    expect(component.fatigueLevel.value).toBe(0);
    expect(component.exerciseTypeId.value).toBe(0);
  });
});
