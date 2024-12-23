import { WorkoutResponseDto } from "../../workouts/models/workout.model";

export interface UserResponseDto {
    id: number;
    username: string;
    firstName?: string;
    lastName?: string;
    email: string;
    createdAt: Date;
    workouts: WorkoutResponseDto[];
  }

export interface UserRequestDto{
  username: string;
  firstName: string;
  lastName: string;
  email: string;
  hashedPassword: string;
}
  