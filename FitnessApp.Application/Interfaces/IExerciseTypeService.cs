using FitnessApp.Domain.Model;

namespace FitnessApp.Application.Interfaces
{
    public interface IExerciseTypeService
    {
        Task<List<ExerciseType>> GetAllExerciseTypesAsync();
        Task<ExerciseType?> GetExerciseTypeByIdAsync(int id);
        Task<ExerciseType?> AddExerciseTypeAsync(ExerciseType exerciseType);
        Task<ExerciseType?> UpdateExerciseTypeAsync(ExerciseType exerciseType);
        Task DeleteExerciseTypeAsync(int id);
    }
}
