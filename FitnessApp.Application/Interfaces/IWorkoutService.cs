using FitnessApp.Domain.Model;

namespace FitnessApp.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task<List<Workout>> GetWorkoutByUserIdAsync(int userId);
        Task<List<Workout>> GetWorkoutByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<Workout?> AddWorkoutAsync(Workout workout);
        Task<Workout?> UpdateWorkoutAsync(Workout workout);
        Task DeleteWorkoutAsync(int workoutId);
    }
}
