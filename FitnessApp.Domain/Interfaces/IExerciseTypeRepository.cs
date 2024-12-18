using FitnessApp.Domain.Model;

namespace FitnessApp.Domain.Interfaces
{
    public interface IExerciseTypeRepository
    {
        Task<List<ExerciseType>> GetAllAsync();
        Task<ExerciseType?> GetByIdAsync(int id);
        Task<ExerciseType?> AddAsync(ExerciseType exerciseType);
        Task<ExerciseType?> UpdateAsync(ExerciseType exerciseType);
        Task DeleteAsync(int id);
    }
}
