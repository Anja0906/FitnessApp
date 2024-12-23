using FitnessApp.Domain.Exceptions;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FitnessApp.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly FitnessAppContext _context;

        public WorkoutRepository(FitnessAppContext context)
        {
            _context = context;
        }

        public async Task<List<Workout>> GetByUserIdAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.ExerciseType)
                .ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(int workoutId)
        {
            var workout = await _context.Workouts
                .Include(w => w.ExerciseType)
                .FirstOrDefaultAsync(w => w.Id == workoutId);
            if (workout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            return workout;
        }

        public async Task<List<Workout>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId && w.DateTime >= startDate && w.DateTime <= endDate)
                .Include(w => w.ExerciseType)
                .ToListAsync();
        }

        public async Task<Workout?> AddAsync(Workout workout)
        {
            if (workout?.UserId == null) 
            {
                throw new EmptyFieldException("User id field can not be empty!");
            }
            if (workout?.ExerciseTypeId == null)
            {
                throw new EmptyFieldException("Exercise type id field can not be empty!");
            }

            if (workout.DateTime.Kind != DateTimeKind.Utc)
            {
                workout.DateTime = workout.DateTime.ToUniversalTime();
            }
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<Workout?> UpdateAsync(Workout workout)
        {
            if (workout?.UserId == null)
            {
                throw new EmptyFieldException("User id field can not be empty!");
            }
            if (workout?.ExerciseTypeId == null)
            {
                throw new EmptyFieldException("Exercise type id field can not be empty!");
            }
            if (workout?.Id == null)
            {
                throw new EmptyFieldException("Id field can not be empty!");
            }
            var existingWorkout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == workout.Id);
            if (existingWorkout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            existingWorkout.Duration = workout.Duration;
            existingWorkout.CaloriesBurned = workout.CaloriesBurned;
            existingWorkout.Intensity = workout.Intensity;
            existingWorkout.FatigueLevel = workout.FatigueLevel;
            existingWorkout.Notes = workout.Notes;
            await _context.SaveChangesAsync();
            return existingWorkout;
        }

        public async Task DeleteAsync(int workoutId)
        {
            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout == null)
            {
                throw new ResourceNotFoundException("Workout with that id does not exist!");
            }
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WeeklyProgress>> GetMonthlyProgressAsync(int userId, int year, int month)
        {
            var workouts = await GetWorkoutsForMonth(userId, year, month);
            var groupedWorkouts = GroupWorkoutsByWeek(workouts);
            var allWeeksInMonth = GetAllWeeksInMonth(year, month);

            return CreateWeeklyProgress(allWeeksInMonth, groupedWorkouts);
        }

        private async Task<List<Workout>> GetWorkoutsForMonth(int userId, int year, int month)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId &&
                            w.DateTime.Year == year &&
                            w.DateTime.Month == month)
                .ToListAsync();
        }

        private Dictionary<int, WeeklyProgress> GroupWorkoutsByWeek(List<Workout> workouts)
        {
            return workouts
                .GroupBy(w => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    w.DateTime,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday))
                .ToDictionary(
                    g => g.Key,
                    g => new WeeklyProgress
                    {
                        Week = g.Key,
                        TotalDuration = g.Sum(w => w.Duration),
                        WorkoutCount = g.Count(),
                        AverageIntensity = g.Average(w => w.Intensity),
                        AverageFatigueLevel = g.Average(w => w.FatigueLevel)
                    });
        }

        private IEnumerable<int> GetAllWeeksInMonth(int year, int month)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            return Enumerable
                .Range(0, (lastDayOfMonth - firstDayOfMonth).Days + 1)
                .Select(d => firstDayOfMonth.AddDays(d))
                .Select(date => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    date,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday))
                .Distinct();
        }

        private List<WeeklyProgress> CreateWeeklyProgress(IEnumerable<int> allWeeks, Dictionary<int, WeeklyProgress> groupedWorkouts)
        {
            return allWeeks
                .Select(week => groupedWorkouts.ContainsKey(week)
                    ? groupedWorkouts[week]
                    : new WeeklyProgress
                    {
                        Week = week,
                        TotalDuration = 0,
                        WorkoutCount = 0,
                        AverageIntensity = 0,
                        AverageFatigueLevel = 0
                    })
                .OrderBy(p => p.Week)
                .ToList();
        }



    }
}
