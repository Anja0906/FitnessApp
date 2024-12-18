using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitnessAppContext _context;

        public WorkoutRepository(FitnessAppContext context)
        {
            _context = context;
        }

        public async Task<List<Workout>> GetByUserIdAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.ExerciseType)
                .ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(int workoutId)
        {
            return await _context.Workouts
                .Include(w => w.ExerciseType)
                .FirstOrDefaultAsync(w => w.Id == workoutId);
        }

        public async Task<List<Workout>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId && w.DateTime >= startDate && w.DateTime <= endDate)
                .Include(w => w.ExerciseType)
                .ToListAsync();
        }

        public async Task<Workout?> AddAsync(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<Workout?> UpdateAsync(Workout workout)
        {
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task DeleteAsync(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                await _context.SaveChangesAsync();
            }
        }
    }
}
