using FitnessApp.Domain.Model;

namespace FitnessApp.Domain.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<List<Workout>> GetByUserIdAsync(int userId);
        Task<Workout?> GetByIdAsync(int userId);
        Task<List<Workout>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<Workout?> AddAsync(Workout workout);
        Task<Workout?> UpdateAsync(Workout workout);
        Task DeleteAsync(int workoutId);
    }
}
