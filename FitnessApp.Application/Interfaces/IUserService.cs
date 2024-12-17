using FitnessApp.Domain.Model;
namespace FitnessApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> LoginUserAsync(string username, string password);
    }
}
