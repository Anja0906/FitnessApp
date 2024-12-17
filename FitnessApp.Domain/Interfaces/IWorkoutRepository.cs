using FitnessApp.Domain.Model;

namespace FitnessApp.Domain.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<List<Workout>> GetByUserIdAsync(int userId);
        Task<List<Workout>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task AddAsync(Workout workout);
        Task UpdateAsync(Workout workout);
        Task DeleteAsync(int workoutId);
    }
}
