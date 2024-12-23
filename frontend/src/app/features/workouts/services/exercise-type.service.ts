import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ExerciseTypeResponseDto } from '../models/exercise-type.model';

@Injectable({
  providedIn: 'root',
})
export class ExerciseTypeService {
    private readonly apiUrl = `${environment.apiUrl}/ExerciseType`;

    constructor(private http: HttpClient) {}

    getExerciseTypes(): Observable<ExerciseTypeResponseDto[]> {
        return this.http.get<ExerciseTypeResponseDto[]>(`${this.apiUrl}`);
    }
}