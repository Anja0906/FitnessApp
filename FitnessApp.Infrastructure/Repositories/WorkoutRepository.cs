using FitnessApp.Domain.Exceptions;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.Infrastructure.Context;
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
            var workout = await _context.Workouts
                .Include(w => w.ExerciseType)
                .FirstOrDefaultAsync(w => w.Id == workoutId);
            if (workout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            return workout;
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
            if (workout?.UserId == null) 
            {
                throw new EmptyFieldException("User id field can not be empty!");
            }
            if (workout?.ExerciseTypeId == null)
            {
                throw new EmptyFieldException("Exercise type id field can not be empty!");
            }
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<Workout?> UpdateAsync(Workout workout)
        {
            if (workout?.UserId == null)
            {
                throw new EmptyFieldException("User id field can not be empty!");
            }
            if (workout?.ExerciseTypeId == null)
            {
                throw new EmptyFieldException("Exercise type id field can not be empty!");
            }
            if (workout?.Id == null)
            {
                throw new EmptyFieldException("Id field can not be empty!");
            }
            var existingWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workout.Id);
            if (existingWorkout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            existingWorkout.Duration = workout.Duration;
            existingWorkout.CaloriesBurned = workout.CaloriesBurned;
            existingWorkout.Intensity = workout.Intensity;
            existingWorkout.FatigueLevel = workout.FatigueLevel;
            existingWorkout.Notes = workout.Notes;
            await _context.SaveChangesAsync();
            return existingWorkout;
        }

        public async Task DeleteAsync(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }
    }
}
