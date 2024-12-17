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

        public async Task DeleteWorkoutAsync(int workoutId)
        {
            await _workoutRepository.DeleteAsync(workoutId);
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
