using FitnessApp.Domain.Model;

namespace FitnessApp.Infrastructure.Context
{
    public static class DbInitializer
    {
        public static void Seed(FitnessAppContext context)
        {
            if (!context.ExerciseTypes.Any())
            {
                context.ExerciseTypes.AddRange(new[]
                {
                new ExerciseType { Id = 1, Name = "Running", Description = "Outdoor running exercise" },
                new ExerciseType { Id = 2, Name = "Swimming", Description = "Swimming in the pool" },
                new ExerciseType { Id = 3, Name = "Cycling", Description = "Outdoor cycling exercise" }
            });
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new[]
                {
                new User { Id = 1, Username = "john_doe", FirstName = "John", LastName = "Doe",
                    HashedPassword = "$2a$10$0.zpHGthXODIO2nOf0nOBuemPSs9itnlgrvNbv5dSu4N5lziD/NBW",
                    Email = "john.doe@example.com", CreatedAt = DateTime.UtcNow },
                new User { Id = 2, Username = "jane_doe", FirstName = "Jane", LastName = "Doe",
                    HashedPassword = "$2a$10$0.zpHGthXODIO2nOf0nOBuemPSs9itnlgrvNbv5dSu4N5lziD/NBW",
                    Email = "jane.doe@example.com", CreatedAt = DateTime.UtcNow }
            });
            }

            if (!context.Workouts.Any())
            {
                context.Workouts.AddRange(new[]
                {
                new Workout { Id = 1, UserId = 1, ExerciseTypeId = 1, Duration = 30, CaloriesBurned = 250,
                    Intensity = 3, FatigueLevel = 2, Notes = "Morning jog", DateTime = DateTime.UtcNow.AddDays(-1) },
                new Workout { Id = 2, UserId = 2, ExerciseTypeId = 2, Duration = 45, CaloriesBurned = 350,
                    Intensity = 4, FatigueLevel = 3, Notes = "Swimming training", DateTime = DateTime.UtcNow.AddDays(-2) }
            });
            }

            context.SaveChanges();
        }
    }

}
