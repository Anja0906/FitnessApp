

    namespace FitnessApp.Domain.Model
    {
        public class User
        {
            public int Id { get; set; }
            public required string Username { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public required string HashedPassword { get; set; }
            public required string Email { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public List<Workout> Workouts { get; set; } = new List<Workout>();

        }
    }
