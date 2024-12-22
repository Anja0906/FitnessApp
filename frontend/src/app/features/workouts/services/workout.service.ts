import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { WorkoutRequestDto, WorkoutResponseDto } from '../models/workout.model';
import { WeeklyProgressResponseDto } from '../../progress/models/weekly-progress.model';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private readonly apiUrl = `${environment.apiUrl}/Workout`;

  constructor(private http: HttpClient) {}

  getWorkouts(userId: number): Observable<WorkoutResponseDto[]> {
    return this.http.get<WorkoutResponseDto[]>(`${this.apiUrl}/user/${userId}`);
  }

  getWorkoutById(id: number): Observable<WorkoutResponseDto> {
    return this.http.get<WorkoutResponseDto>(`${this.apiUrl}/${id}`);
  }

  addWorkout(workout: WorkoutRequestDto): Observable<WorkoutResponseDto> {
    return this.http.post<WorkoutResponseDto>(`${this.apiUrl}`, workout);
  }
  

  getMonthlyProgress(userId: number, year: number, month: number): Observable<WeeklyProgressResponseDto[]> {
    const url = `${this.apiUrl}/user/${userId}/progress?year=${year}&month=${month}`;
    return this.http.get<WeeklyProgressResponseDto[]>(url);
  }
}
