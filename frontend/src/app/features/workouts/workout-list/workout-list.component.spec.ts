import { ExerciseTypeResponseDto } from './../models/exercise-type.model';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WorkoutListComponent } from './workout-list.component';
import { WorkoutService } from '../services/workout.service';
import { of } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { WorkoutResponseDto } from '../models/workout.model';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('WorkoutListComponent', () => {
  let component: WorkoutListComponent;
  let fixture: ComponentFixture<WorkoutListComponent>;
  let mockWorkoutService: jasmine.SpyObj<WorkoutService>;

  const mockWorkouts: WorkoutResponseDto[] = [
    {
      id: 1,
      userId: 1,
      exerciseTypeId: 1,
      duration: 30,
      caloriesBurned: 200,
      intensity: 5,
      fatigueLevel: 3,
      notes: 'Morning run',
      dateTime: new Date('2024-01-01T07:00:00'),
      exerciseType: { id: 1, name: 'Running', description: 'Cardio exercise' },
    },
    {
      id: 2,
      userId: 1,
      exerciseTypeId: 2,
      duration: 45,
      caloriesBurned: 300,
      intensity: 6,
      fatigueLevel: 2,
      notes: 'Evening cycling',
      dateTime: new Date('2024-01-01T18:00:00'),
      exerciseType: { id: 2, name: 'Cycling', description: 'Outdoor exercise' },
    },
  ];

  beforeEach(async () => {
    mockWorkoutService = jasmine.createSpyObj('WorkoutService', ['getWorkouts', 'addWorkout']);
    mockWorkoutService.getWorkouts.and.returnValue(of(mockWorkouts));

    spyOn(localStorage, 'getItem').and.returnValue('1'); 

    await TestBed.configureTestingModule({
      imports: [
        WorkoutListComponent, 
        MatIconModule,
      ],
      providers: [{ provide: WorkoutService, useValue: mockWorkoutService }],
      schemas: [NO_ERRORS_SCHEMA],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should fetch and display workouts', () => {
    expect(component.workouts.length).toBe(2);
    const compiled = fixture.nativeElement;
    expect(compiled.querySelectorAll('.workout-card').length).toBe(2);
    expect(compiled.querySelector('.workout-card').textContent).toContain('Running');
  });

  it('should select a workout on click', () => {
    const workout = mockWorkouts[0];
    component.selectWorkout(workout);
    expect(component.selectedWorkout).toBe(workout);
  });

  it('should add a workout and refresh the list', () => {
    const newWorkout: WorkoutResponseDto = {
      id: 3,
      userId: 1,
      exerciseTypeId: 3,
      duration: 60,
      caloriesBurned: 400,
      intensity: 7,
      fatigueLevel: 4,
      notes: 'Swimming session',
      dateTime: new Date('2024-01-02T10:00:00'),
      exerciseType: { id: 3, name: 'Swimming', description: 'Water-based exercise' },
    };

    mockWorkoutService.getWorkouts.and.returnValue(of([...mockWorkouts, newWorkout]));
    component.onWorkoutAdded(newWorkout);

    expect(component.workouts.length).toBe(3);
    expect(component.selectedWorkout).toEqual(newWorkout);
  });

  it('should paginate workouts', () => {
    component.workouts = Array.from({ length: 25 }, (_, i) => ({
      id: i + 1,
      userId: 1,
      exerciseTypeId: i + 1,
      duration: 30,
      caloriesBurned: 200,
      intensity: 5,
      fatigueLevel: 3,
      notes: `Workout ${i + 1}`,
      dateTime: new Date('2024-01-01T07:00:00'),
      exerciseType: { id: i + 1, name: `Workout ${i + 1}`, description: 'Test workout' },
    }));
    component.currentPage = 2;

    const paginatedWorkouts = component.paginatedWorkouts;
    expect(paginatedWorkouts.length).toBe(component.itemsPerPage);
    expect(paginatedWorkouts[0].exerciseType.name).toBe('Workout 11');
  });
});
