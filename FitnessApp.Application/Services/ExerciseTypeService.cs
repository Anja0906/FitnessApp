using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;

namespace FitnessApp.Application.Services
{
    public class ExerciseTypeService : IExerciseTypeService
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        public ExerciseTypeService(IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseTypeRepository = exerciseTypeRepository;
        }
        
        public async Task<ExerciseType?> AddExerciseTypeAsync(ExerciseType exerciseType)
        {
            return await _exerciseTypeRepository.AddAsync(exerciseType);
        }

        public async Task DeleteExerciseTypeAsync(int id)
        {
            await _exerciseTypeRepository.DeleteAsync(id);
        }

        public async Task<List<ExerciseType>> GetAllExerciseTypesAsync()
        {
            return await _exerciseTypeRepository.GetAllAsync();
        }

        public async Task<ExerciseType?> GetExerciseTypeByIdAsync(int id)
        {
            return await _exerciseTypeRepository.GetByIdAsync(id);
        }

        public async Task<ExerciseType?> UpdateExerciseTypeAsync(ExerciseType exerciseType)
        {
            return await _exerciseTypeRepository.UpdateAsync(exerciseType);
        }
    }
}
