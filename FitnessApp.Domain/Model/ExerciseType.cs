
namespace FitnessApp.Domain.Model
{
    public class ExerciseType
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
