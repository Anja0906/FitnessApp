namespace FitnessApp.WebApi.DTOs.Requests
{
    public class ExerciseTypeUpdateRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
