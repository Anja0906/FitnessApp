import { CommonModule, DatePipe } from '@angular/common';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WorkoutDetailComponent } from './workout-detail.component';
import { WorkoutResponseDto } from '../models/workout.model';

describe('WorkoutDetailComponent', () => {
  let component: WorkoutDetailComponent;
  let fixture: ComponentFixture<WorkoutDetailComponent>;

  const mockWorkout: WorkoutResponseDto = {
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
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule, WorkoutDetailComponent], 
      providers: [DatePipe],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutDetailComponent);
    component = fixture.componentInstance;
    component.workout = mockWorkout;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display workout details', () => {
    const compiled = fixture.nativeElement;

    expect(compiled.textContent).toContain('Running');
    expect(compiled.textContent).toContain('Morning run');
    expect(compiled.textContent).toContain('200');
    expect(compiled.textContent).toContain('5');
    expect(compiled.textContent).toContain('3');
  });

  it('should display formatted date and time', () => {
    const compiled = fixture.nativeElement;
    const datePipe = new DatePipe('en-US');
    const formattedDate = datePipe.transform(mockWorkout.dateTime, 'dd.MM.yyyy HH:mm:ss');
    expect(compiled.textContent).toContain(formattedDate);
  });

  it('should render default values if no workout is provided', () => {
    component.workout = null as any; 
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('No workout details available.');
  });
});
