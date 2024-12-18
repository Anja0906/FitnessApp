using FitnessApp.WebApi.DTOs.Responses;

namespace FitnessApp.WebApi.DTOs.Requests
{
    public class WorkoutUpdateRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExerciseTypeId { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; }
        public int FatigueLevel { get; set; }
        public string? Notes { get; set; }
    }
}
