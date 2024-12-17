

namespace FitnessApp.Domain.Model
{
    public class Workout
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public User? User { get; set; }
        public required int ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }
        public int Duration { get; set; } 
        public int CaloriesBurned { get; set; }
        public int Intensity { get; set; } 
        public int FatigueLevel { get; set; } 
        public string? Notes { get; set; }
        public DateTime DateTime { get; set; }

    }
}
