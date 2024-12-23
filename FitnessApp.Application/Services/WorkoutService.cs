using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;

namespace FitnessApp.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        public readonly IWorkoutRepository _workoutRepository;
        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        public async Task<Workout?> AddWorkoutAsync(Workout workout)
        {
            return await _workoutRepository.AddAsync(workout);
        }

        public async Task DeleteWorkoutAsync(int workoutId, int loggedInUserId)
        {
            var usersWorkouts = await _workoutRepository.GetByUserIdAsync(loggedInUserId);
            bool workoutExists = false;
            foreach (var workout in usersWorkouts) { 
                if (workout.Id == workoutId)
                {
                    workoutExists = true;
                }
            }
            if (workoutExists == false) {
                throw new Exception("Workout does not exist!");
            }
            else
            {
                await _workoutRepository.DeleteAsync(workoutId);
            }
        }

        public async Task<List<WeeklyProgress>> GetMonthlyProgressAsync(int userId, int year, int month)
        {
            return await _workoutRepository.GetMonthlyProgressAsync(userId, year, month);
        }

        public async Task<List<Workout>> GetWorkoutByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _workoutRepository.GetByDateRangeAsync(userId, startDate, endDate);
        }

        public async Task<List<Workout>> GetWorkoutByUserIdAsync(int userId)
        {
            return await _workoutRepository.GetByUserIdAsync(userId);
        }

        public async Task<Workout?> UpdateWorkoutAsync(Workout workout)
        {
            return await _workoutRepository.UpdateAsync(workout);
        }
    }
}
