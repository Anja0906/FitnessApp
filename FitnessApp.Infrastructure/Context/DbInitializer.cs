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
                    new User
                    {
                        Id = 1, Username = "john.doe@example.com", FirstName = "John", LastName = "Doe",
                        HashedPassword = "$2a$10$0.zpHGthXODIO2nOf0nOBuemPSs9itnlgrvNbv5dSu4N5lziD/NBW",
                        Email = "john.doe@example.com", CreatedAt = DateTime.UtcNow
                    },
                    new User
                    {
                        Id = 2, Username = "jane.doe@example.com", FirstName = "Jane", LastName = "Doe",
                        HashedPassword = "$2a$10$0.zpHGthXODIO2nOf0nOBuemPSs9itnlgrvNbv5dSu4N5lziD/NBW",
                        Email = "jane.doe@example.com", CreatedAt = DateTime.UtcNow
                    }
                });
            }

            if (!context.Workouts.Any())
            {
                context.Workouts.AddRange(new[]
                {
                    new Workout
                    {
                        Id = 1, UserId = 1, ExerciseTypeId = 1, Duration = 30, CaloriesBurned = 250,
                        Intensity = 3, FatigueLevel = 2, Notes = "Morning jog", DateTime = DateTime.UtcNow.AddDays(-1)
                    },
                    new Workout
                    {
                        Id = 2, UserId = 2, ExerciseTypeId = 2, Duration = 45, CaloriesBurned = 350,
                        Intensity = 4, FatigueLevel = 3, Notes = "Swimming training", DateTime = DateTime.UtcNow.AddDays(-2)
                    },
                    new Workout
                    {
                        Id = 3, UserId = 1, ExerciseTypeId = 3, Duration = 60, CaloriesBurned = 500,
                        Intensity = 5, FatigueLevel = 4, Notes = "Cycling session", DateTime = DateTime.UtcNow.AddDays(-3)
                    },
                    new Workout
                    {
                        Id = 4, UserId = 2, ExerciseTypeId = 1, Duration = 25, CaloriesBurned = 200,
                        Intensity = 2, FatigueLevel = 1, Notes = "Light jog", DateTime = DateTime.UtcNow.AddDays(-4)
                    },
                    new Workout
                    {
                        Id = 5, UserId = 1, ExerciseTypeId = 2, Duration = 40, CaloriesBurned = 320,
                        Intensity = 3, FatigueLevel = 3, Notes = "Swimming practice", DateTime = DateTime.UtcNow.AddDays(-5)
                    },
                    new Workout
                    {
                        Id = 6, UserId = 2, ExerciseTypeId = 3, Duration = 70, CaloriesBurned = 600,
                        Intensity = 6, FatigueLevel = 5, Notes = "Intense cycling", DateTime = DateTime.UtcNow.AddDays(-6)
                    }
                });
            }

            context.SaveChanges();
        }
    }
}
