using FitnessApp.Domain.Exceptions;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FitnessAppContext _context;

        public UserRepository(FitnessAppContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Workouts)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) 
            {
                throw new ResourceNotFoundException("User with that id does not exist!");
            }
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new ResourceNotFoundException("User with that username does not exist!");
            }
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new ResourceNotFoundException("User with that email does not exist!");
            }
            return user;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> AddAsync(User user)
        {
            bool existWithEmail = await ExistsByEmailAsync(user.Email);
            if (existWithEmail) 
            {
                throw new UserExistsException($"User with email: {user.Email} already exists!");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null)
            {
                throw new ResourceNotFoundException("User with that id does not exist!");
            }
            existingUser.Username = user.Username;
            existingUser.FirstName = user.FirstName;
            existingUser.HashedPassword = user.HashedPassword;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
