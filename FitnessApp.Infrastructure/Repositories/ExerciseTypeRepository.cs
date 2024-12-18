using FitnessApp.Domain.Exceptions;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Infrastructure.Repositories
{
    public class ExerciseTypeRepository : IExerciseTypeRepository
    {
        private readonly FitnessAppContext _context;

        public ExerciseTypeRepository(FitnessAppContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseType>> GetAllAsync()
        {
            return await _context.ExerciseTypes.ToListAsync();
        }

        public async Task<ExerciseType?> GetByIdAsync(int id)
        {
            var exerciseType = await _context.ExerciseTypes.FirstOrDefaultAsync(e => e.Id == id);
            if (exerciseType == null)
            {
                throw new ResourceNotFoundException("Exercise type with that id does not exist!");
            }
            return exerciseType;
        }

        public async Task<ExerciseType?> AddAsync(ExerciseType exerciseType)
        {
            _context.ExerciseTypes.Add(exerciseType);
            await _context.SaveChangesAsync();
            return exerciseType;
        }

        public async Task<ExerciseType?> UpdateAsync(ExerciseType exerciseType)
        {
            var existingExerciseType = await _context.ExerciseTypes.FirstOrDefaultAsync(e => e.Id == exerciseType.Id);
            if (existingExerciseType == null)
            {
                throw new ResourceNotFoundException("Exercise type with that id does not exist!");
            }
            existingExerciseType.Name = exerciseType.Name;
            existingExerciseType.Description = exerciseType.Description;
            await _context.SaveChangesAsync();
            return existingExerciseType;
        }

        public async Task DeleteAsync(int id)
        {
            var exerciseType = await _context.ExerciseTypes.FindAsync(id);
            if (exerciseType == null)
            {
                throw new ResourceNotFoundException("Exercise type with that id does not exist!");
            }
            _context.ExerciseTypes.Remove(exerciseType);
            await _context.SaveChangesAsync();
        }
    }
}
