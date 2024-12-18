namespace FitnessApp.WebApi.DTOs.Requests
{
    public class UserUpdateRequestDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
    }
}
