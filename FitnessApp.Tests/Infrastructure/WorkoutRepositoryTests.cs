using FitnessApp.Domain.Exceptions;
using FitnessApp.Domain.Model;
using Xunit;

namespace FitnessApp.Tests.Infrastructure
{
    public class WorkoutRepositoryTests
    {
        private readonly WorkoutRepositoryTestSetup _setup;

        public WorkoutRepositoryTests()
        {
            _setup = new WorkoutRepositoryTestSetup();
        }

        private void SeedData()
        {
            var exerciseTypes = new List<ExerciseType>
            {
                new ExerciseType { Name = "Running", Description = "Outdoor running exercise" },
                new ExerciseType { Name = "Swimming", Description = "Pool swimming exercise" }
            };

            var users = new List<User>
            {
                new User { Username = "john.doe", Email = "john.doe@example.com", HashedPassword = "hashed_password" }
            };

            var workouts = new List<Workout>
            {
                new Workout { UserId = 1, ExerciseTypeId = 1, Duration = 30, Intensity = 3, FatigueLevel = 2, Notes = "Morning jog", DateTime = new DateTime(2024, 1, 1) },
                new Workout { UserId = 1, ExerciseTypeId = 2, Duration = 45, Intensity = 4, FatigueLevel = 3, Notes = "Swimming session", DateTime = new DateTime(2024, 1, 2) },
        
                new Workout { UserId = 1, ExerciseTypeId = 1, Duration = 20, DateTime = new DateTime(2024, 2, 1) }, // Wrong month
                new Workout { UserId = 2, ExerciseTypeId = 2, Duration = 40, DateTime = new DateTime(2024, 1, 15) } // Wrong user
            };

            _setup.DataContext.ExerciseTypes.AddRange(exerciseTypes);
            _setup.DataContext.Users.AddRange(users);
            _setup.DataContext.Workouts.AddRange(workouts);
            _setup.DataContext.SaveChanges();
        }



        [Fact]
        public async Task GetByUserIdAsync_ValidUserId_ShouldReturnWorkouts()
        {
            _setup.InitializeDataContext(nameof(GetByUserIdAsync_ValidUserId_ShouldReturnWorkouts));
            SeedData();

            var result = await _setup.WorkoutRepository.GetByUserIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.All(result, w => Assert.Equal(1, w.UserId));
        }

        [Fact]
        public async Task GetByUserIdAsync_InvalidUserId_ShouldReturnEmptyList()
        {
            _setup.InitializeDataContext(nameof(GetByUserIdAsync_InvalidUserId_ShouldReturnEmptyList));
            SeedData();

            var result = await _setup.WorkoutRepository.GetByUserIdAsync(999); 

            Assert.NotNull(result);
            Assert.Empty(result); 
        }


        [Fact]
        public async Task AddAsync_ValidWorkout_ShouldAddWorkout()
        {
            _setup.InitializeDataContext(nameof(AddAsync_ValidWorkout_ShouldAddWorkout));
            SeedData();

            var workout = new Workout
            {
                UserId = 1,
                ExerciseTypeId = 1,
                Duration = 60,
                CaloriesBurned = 300,
                Intensity = 4,
                FatigueLevel = 2,
                Notes = "Evening run",
                DateTime = DateTime.UtcNow
            };

            var result = await _setup.WorkoutRepository.AddAsync(workout);

            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("Evening run", result.Notes);
        }




        [Fact]
        public async Task GetByIdAsync_ValidId_ShouldReturnWorkout()
        {
            _setup.InitializeDataContext(nameof(GetByIdAsync_ValidId_ShouldReturnWorkout));
            SeedData();

            var result = await _setup.WorkoutRepository.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Morning jog", result.Notes);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ShouldThrowResourceNotFoundException()
        {
            _setup.InitializeDataContext(nameof(GetByIdAsync_InvalidId_ShouldThrowResourceNotFoundException));
            SeedData();

            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
                _setup.WorkoutRepository.GetByIdAsync(999));

            Assert.Equal("Workout with that id does not exist!", exception.Message);
        }

        [Fact]
        public async Task GetByDateRangeAsync_ValidParameters_ShouldReturnWorkoutsInRange()
        {
            _setup.InitializeDataContext(nameof(GetByDateRangeAsync_ValidParameters_ShouldReturnWorkoutsInRange));
            SeedData();

            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 31);

            var result = await _setup.WorkoutRepository.GetByDateRangeAsync(1, startDate, endDate);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, w => Assert.InRange(w.DateTime, startDate, endDate));
        }

        [Fact]
        public async Task GetByDateRangeAsync_StartDateAfterEndDate_ShouldReturnEmptyList()
        {
            _setup.InitializeDataContext(nameof(GetByDateRangeAsync_StartDateAfterEndDate_ShouldReturnEmptyList));
            SeedData();

            var result = await _setup.WorkoutRepository.GetByDateRangeAsync(1, DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByDateRangeAsync_ValidRange_NoWorkoutsForUser_ShouldReturnEmptyList()
        {
            _setup.InitializeDataContext(nameof(GetByDateRangeAsync_ValidRange_NoWorkoutsForUser_ShouldReturnEmptyList));
            SeedData();

            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 1, 31);

            var result = await _setup.WorkoutRepository.GetByDateRangeAsync(1, startDate, endDate);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByDateRangeAsync_NonExistentUser_ShouldReturnEmptyList()
        {
            _setup.InitializeDataContext(nameof(GetByDateRangeAsync_NonExistentUser_ShouldReturnEmptyList));
            SeedData();

            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 31);

            var result = await _setup.WorkoutRepository.GetByDateRangeAsync(999, startDate, endDate); 

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_ValidWorkout_ShouldUpdateWorkout()
        {
            _setup.InitializeDataContext(nameof(UpdateAsync_ValidWorkout_ShouldUpdateWorkout));
            SeedData();

            var workout = new Workout
            {
                Id = 1,
                UserId = 1,
                ExerciseTypeId = 1,
                Duration = 60,
                Notes = "Updated notes",
                DateTime = DateTime.UtcNow
            };

            var result = await _setup.WorkoutRepository.UpdateAsync(workout);

            Assert.NotNull(result);
            Assert.Equal("Updated notes", result.Notes);
        }


        [Fact]
        public async Task UpdateAsync_NonExistentWorkout_ShouldThrowResourceNotFoundException()
        {
            _setup.InitializeDataContext(nameof(UpdateAsync_NonExistentWorkout_ShouldThrowResourceNotFoundException));
            SeedData();

            var workout = new Workout
            {
                Id = 999, 
                UserId = 1,
                ExerciseTypeId = 1,
                Duration = 60,
                Notes = "Updated notes",
                DateTime = DateTime.UtcNow
            };

            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
                _setup.WorkoutRepository.UpdateAsync(workout));

            Assert.Equal("Workout with that id does not exist!", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ShouldDeleteWorkout()
        {
            _setup.InitializeDataContext(nameof(DeleteAsync_ValidId_ShouldDeleteWorkout));
            SeedData();

            await _setup.WorkoutRepository.DeleteAsync(1);

            var result = await _setup.DataContext.Workouts.FindAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_InvalidWorkoutId_ShouldThrowResourceNotFoundException()
        {
            _setup.InitializeDataContext(nameof(DeleteAsync_InvalidWorkoutId_ShouldThrowResourceNotFoundException));
            SeedData(); 

            var workoutId = 999; 

            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
                _setup.WorkoutRepository.DeleteAsync(workoutId));

            Assert.Equal("Workout with that id does not exist!", exception.Message); 
        }


        [Fact]
        public async Task GetMonthlyProgressAsync_ValidParameters_ShouldReturnCorrectProgress()
        {
            _setup.InitializeDataContext(nameof(GetMonthlyProgressAsync_ValidParameters_ShouldReturnCorrectProgress));
            SeedData();

            var result = await _setup.WorkoutRepository.GetMonthlyProgressAsync(1, 2024, 1);

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); 
            Assert.Equal(75, result[0].TotalDuration);
            Assert.Equal(3.5, result[0].AverageIntensity); 
            Assert.Equal(2.5, result[0].AverageFatigueLevel); 
        }

        [Fact]
        public async Task GetMonthlyProgressAsync_NoWorkouts_ShouldReturnEmptyProgress()
        {
            _setup.InitializeDataContext(nameof(GetMonthlyProgressAsync_NoWorkouts_ShouldReturnEmptyProgress));
            SeedData();

            var result = await _setup.WorkoutRepository.GetMonthlyProgressAsync(1, 2024, 3); 

            Assert.NotNull(result);
            Assert.All(result, progress =>
            {
                Assert.Equal(0, progress.TotalDuration);
                Assert.Equal(0, progress.WorkoutCount);
                Assert.Equal(0, progress.AverageIntensity);
                Assert.Equal(0, progress.AverageFatigueLevel);
            });
        }

        [Fact]
        public async Task GetMonthlyProgressAsync_NonExistentUser_ShouldReturnEmptyProgress()
        {
            _setup.InitializeDataContext(nameof(GetMonthlyProgressAsync_NonExistentUser_ShouldReturnEmptyProgress));
            SeedData();

            var result = await _setup.WorkoutRepository.GetMonthlyProgressAsync(999, 2024, 1);

            Assert.NotNull(result);
            Assert.All(result, progress =>
            {
                Assert.Equal(0, progress.TotalDuration);
                Assert.Equal(0, progress.WorkoutCount);
                Assert.Equal(0, progress.AverageIntensity);
                Assert.Equal(0, progress.AverageFatigueLevel);
            });
        }

        [Fact]
        public async Task GetMonthlyProgressAsync_InvalidYearOrMonth_ShouldThrowArgumentException()
        {
            _setup.InitializeDataContext(nameof(GetMonthlyProgressAsync_InvalidYearOrMonth_ShouldThrowArgumentException));
            SeedData();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _setup.WorkoutRepository.GetMonthlyProgressAsync(1, -1, 1));

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _setup.WorkoutRepository.GetMonthlyProgressAsync(1, 2024, 13));
        }


    }
}
