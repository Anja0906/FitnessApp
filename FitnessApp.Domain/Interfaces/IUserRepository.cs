using FitnessApp.Domain.Model;

namespace FitnessApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<User?> AddAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}
