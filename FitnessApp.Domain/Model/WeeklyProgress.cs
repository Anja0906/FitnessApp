namespace FitnessApp.Domain.Model
{
    public class WeeklyProgress
    {
        public int Week { get; set; }
        public int TotalDuration { get; set; }
        public int WorkoutCount { get; set; }
        public double AverageIntensity { get; set; }
        public double AverageFatigueLevel { get; set; }
    }

}
