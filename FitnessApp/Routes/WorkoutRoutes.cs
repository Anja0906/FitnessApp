namespace FitnessApp.WebApi.Routes
{
    public static class WorkoutRoutes
    {
        public const string Base = "api/Workout";
        public const string GetWorkoutsByUser = "user/{userId}";
        public const string GetWorkoutsByDateRange = "user/{userId}/date-range";
        public const string AddWorkout = "";
        public const string UpdateWorkout = "{id}";
        public const string DeleteWorkout = "{id}";
        public const string GetMonthlyProgress = "user/{userId}/progress";
    }
}
