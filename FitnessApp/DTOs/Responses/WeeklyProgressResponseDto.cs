namespace FitnessApp.WebApi.DTOs.Responses
{
    public class WeeklyProgressResponseDto
    {
        public int Week { get; set; }
        public int TotalDuration { get; set; }
        public int WorkoutCount { get; set; }
        public double AverageIntensity { get; set; }
        public double AverageFatigueLevel { get; set; }
    }
}
