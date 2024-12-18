namespace FitnessApp.WebApi.DTOs.Requests
{
    public class WorkoutRequestDto
    {
        public int UserId { get; set; }
        public int ExerciseTypeId { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; }
        public int FatigueLevel { get; set; }
        public string? Notes { get; set; }
        public DateTime DateTime { get; set; }
    }
}
