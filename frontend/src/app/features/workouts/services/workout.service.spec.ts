import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WorkoutService } from './workout.service';
import { WorkoutRequestDto, WorkoutResponseDto } from '../models/workout.model';
import { WeeklyProgressResponseDto } from '../../progress/models/weekly-progress.model';
import { environment } from '../../../../environments/environment';

describe('WorkoutService', () => {
  let service: WorkoutService;
  let httpTestingController: HttpTestingController;

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
      dateTime: new Date(),
      exerciseType: { id: 1, name: 'Running', description: 'Cardio exercise' },
    },
  ];

  const mockProgress: WeeklyProgressResponseDto[] = [
    {
      week: 1,
      totalDuration: 120,
      workoutCount: 5,
      averageIntensity: 6,
      averageFatigueLevel: 4,
    },
    {
      week: 2,
      totalDuration: 150,
      workoutCount: 7,
      averageIntensity: 7,
      averageFatigueLevel: 3,
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WorkoutService],
    });
    service = TestBed.inject(WorkoutService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify(); 
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch workouts for a user', () => {
    const userId = 1;

    service.getWorkouts(userId).subscribe((workouts) => {
      expect(workouts).toEqual(mockWorkouts);
      expect(workouts.length).toBe(1);
      expect(workouts[0].userId).toBe(userId);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/Workout/user/${userId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockWorkouts);
  });

  it('should fetch a workout by ID', () => {
    const workoutId = 1;

    service.getWorkoutById(workoutId).subscribe((workout) => {
      expect(workout).toEqual(mockWorkouts[0]);
      expect(workout.id).toBe(workoutId);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/Workout/${workoutId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockWorkouts[0]);
  });

  it('should add a new workout', () => {
    const newWorkout: WorkoutRequestDto = {
      userId: 1,
      exerciseTypeId: 2,
      duration: 45,
      caloriesBurned: 300,
      intensity: 6,
      fatigueLevel: 2,
      notes: 'Evening cycling',
      dateTime: new Date(),
    };

    service.addWorkout(newWorkout).subscribe((addedWorkout) => {
      expect(addedWorkout).toEqual(mockWorkouts[0]);
      expect(addedWorkout.userId).toBe(newWorkout.userId);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/Workout`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newWorkout);
    req.flush(mockWorkouts[0]);
  });

  it('should fetch monthly progress for a user', () => {
    const userId = 1;
    const year = 2024;
    const month = 1;

    service.getMonthlyProgress(userId, year, month).subscribe((progress) => {
      expect(progress).toEqual(mockProgress);
      expect(progress.length).toBe(2);

      expect(progress[0].week).toBe(1);
      expect(progress[0].totalDuration).toBe(120);
      expect(progress[0].workoutCount).toBe(5);
      expect(progress[0].averageIntensity).toBe(6);
      expect(progress[0].averageFatigueLevel).toBe(4);
    });

    const req = httpTestingController.expectOne(
      `${environment.apiUrl}/Workout/user/${userId}/progress?year=${year}&month=${month}`
    );
    expect(req.request.method).toBe('GET');
    req.flush(mockProgress);
  });
});
