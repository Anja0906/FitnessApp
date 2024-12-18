using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;

namespace FitnessApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> LoginUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.HashedPassword))
            {
                return null; // Invalid username or password
            }

            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> AddUserAsync(User user)
        {
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
            return await _userRepository.AddAsync(user);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
