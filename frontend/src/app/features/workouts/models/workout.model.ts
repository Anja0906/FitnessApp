import { ExerciseTypeResponseDto } from "./exercise-type.model";

export interface WorkoutResponseDto {
    id: number;
    userId: number;
    exerciseTypeId: number;
    duration: number;
    caloriesBurned: number;
    intensity: number;
    fatigueLevel: number;
    notes?: string;
    dateTime: Date;
    exerciseType: ExerciseTypeResponseDto;
}

export interface WorkoutRequestDto {
    userId: number;
    exerciseTypeId: number;
    duration: number;
    caloriesBurned: number;
    intensity: number;
    fatigueLevel: number;
    notes?: string;
    dateTime: Date;
}

