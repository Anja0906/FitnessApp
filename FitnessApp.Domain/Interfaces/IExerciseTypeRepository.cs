using FitnessApp.Domain.Model;

namespace FitnessApp.Domain.Interfaces
{
    public interface IExerciseTypeRepository
    {
        Task<List<ExerciseType>> GetAllAsync();
        Task<ExerciseType?> GetByIdAsync(int id);
        Task AddAsync(ExerciseType exerciseType);
        Task UpdateAsync(ExerciseType exerciseType);
        Task DeleteAsync(int id);
    }
}
