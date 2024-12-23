﻿namespace FitnessApp.WebApi.DTOs.Responses
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<WorkoutResponseDto> Workouts { get; set; } = new();
    }
}
