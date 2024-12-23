import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ExerciseTypeService } from './exercise-type.service';
import { ExerciseTypeResponseDto } from '../models/exercise-type.model';
import { environment } from '../../../../environments/environment';

describe('ExerciseTypeService', () => {
  let service: ExerciseTypeService;
  let httpTestingController: HttpTestingController;

  const mockExerciseTypes: ExerciseTypeResponseDto[] = [
    { id: 1, name: 'Running', description: 'Cardio exercise' },
    { id: 2, name: 'Cycling', description: 'Outdoor exercise' },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ExerciseTypeService],
    });
    service = TestBed.inject(ExerciseTypeService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify(); 
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch exercise types', () => {
    service.getExerciseTypes().subscribe((exerciseTypes) => {
      expect(exerciseTypes).toEqual(mockExerciseTypes);
      expect(exerciseTypes.length).toBe(2);
      expect(exerciseTypes[0].name).toBe('Running');
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/ExerciseType`);
    expect(req.request.method).toBe('GET');
    req.flush(mockExerciseTypes); 
  });
});
