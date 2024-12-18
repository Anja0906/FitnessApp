namespace FitnessApp.WebApi.DTOs.Responses
{
    public class WorkoutResponseDto
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; }
        public int FatigueLevel { get; set; }
        public string? Notes { get; set; }
        public DateTime DateTime { get; set; }
        public ExerciseTypeResponseDto ExerciseType { get; set; }
    }
}
