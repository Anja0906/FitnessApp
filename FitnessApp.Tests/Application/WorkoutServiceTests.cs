using FitnessApp.Domain.Model;
using Moq;

namespace FitnessApp.Tests.Application
{
    public class WorkoutServiceTests
    {
        private readonly WorkoutServiceTestSetup _setup;

        public WorkoutServiceTests()
        {
            _setup = new WorkoutServiceTestSetup();
        }

        [Fact]
        public async Task AddWorkoutAsync_ValidWorkout_ShouldAddWorkout()
        {
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

            _setup.SetupAddWorkout(workout, workout);

            var result = await _setup.WorkoutService.AddWorkoutAsync(workout);

            _setup.WorkoutRepositoryMock.Verify(repo => repo.AddAsync(workout), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(workout.Notes, result.Notes);
        }

        [Fact]
        public async Task AddWorkoutAsync_NullWorkout_ShouldThrowArgumentNullException()
        {
            Workout workout = null;
            _setup.SetupAddWorkout(workout, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => _setup.WorkoutService.AddWorkoutAsync(workout));
        }



        [Fact]
        public async Task DeleteWorkoutAsync_ValidWorkout_ShouldDeleteWorkout()
        {
            int workoutId = 1;
            int loggedInUserId = 1;
            var workouts = new List<Workout>
            {
                new Workout { Id = workoutId, UserId = loggedInUserId, ExerciseTypeId = 1, Duration = 30, Intensity = 3, FatigueLevel = 2, Notes = "Morning jog", DateTime = new DateTime(2024, 1, 1) },
            };
            _setup.SetupGetByUserId(loggedInUserId, workouts);
            _setup.SetupDeleteWorkout(workoutId);

            await _setup.WorkoutService.DeleteWorkoutAsync(workoutId, loggedInUserId);

            _setup.WorkoutRepositoryMock.Verify(repo => repo.DeleteAsync(workoutId), Times.Once);
        }

        [Fact]
        public async Task DeleteWorkoutAsync_InvalidWorkout_ShouldThrowException()
        {
            int workoutId = 1;
            int loggedInUserId = 1;
            _setup.SetupGetByUserId(loggedInUserId, new List<Workout>());

            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _setup.WorkoutService.DeleteWorkoutAsync(workoutId, loggedInUserId));

            Assert.Equal("Workout does not exist!", exception.Message);
        }

        [Fact]
        public async Task GetWorkoutByDateRangeAsync_ValidRange_ShouldReturnWorkouts()
        {
            int userId = 1;
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 1, 31);
            var workouts = new List<Workout>
            {
                new Workout { Id = 1, UserId = userId, ExerciseTypeId = 1, Duration = 30, Notes = "Morning jog", DateTime = new DateTime(2024, 1, 1) },
                new Workout { Id = 2, UserId = userId, ExerciseTypeId = 2, Duration = 45, Notes = "Swimming session", DateTime = new DateTime(2024, 1, 15) }
            };

            _setup.WorkoutRepositoryMock.Setup(repo => repo.GetByDateRangeAsync(userId, startDate, endDate)).ReturnsAsync(workouts);

            var result = await _setup.WorkoutService.GetWorkoutByDateRangeAsync(userId, startDate, endDate);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _setup.WorkoutRepositoryMock.Verify(repo => repo.GetByDateRangeAsync(userId, startDate, endDate), Times.Once);
        }

        [Fact]
        public async Task GetWorkoutByDateRangeAsync_InvalidDateRange_ShouldReturnEmptyList()
        {
            int userId = 1;
            var startDate = new DateTime(2023, 12, 1);
            var endDate = new DateTime(2023, 12, 31); 
            _setup.WorkoutRepositoryMock.Setup(repo => repo.GetByDateRangeAsync(userId, startDate, endDate)).ReturnsAsync(new List<Workout>());

            var result = await _setup.WorkoutService.GetWorkoutByDateRangeAsync(userId, startDate, endDate);

            Assert.NotNull(result);
            Assert.Empty(result);
            _setup.WorkoutRepositoryMock.Verify(repo => repo.GetByDateRangeAsync(userId, startDate, endDate), Times.Once);
        }



        [Fact]
        public async Task GetMonthlyProgressAsync_ValidParameters_ShouldReturnProgress()
        {
            int userId = 1;
            int year = 2024;
            int month = 1;
            var progress = new List<WeeklyProgress>
            {
                new WeeklyProgress { Week = 1, TotalDuration = 120, WorkoutCount = 2, AverageIntensity = 3.5, AverageFatigueLevel = 2.5 },
                new WeeklyProgress { Week = 2, TotalDuration = 90, WorkoutCount = 1, AverageIntensity = 4.0, AverageFatigueLevel = 3.0 }
            };
            _setup.SetupGetMonthlyProgress(userId, year, month, progress);

            var result = await _setup.WorkoutService.GetMonthlyProgressAsync(userId, year, month);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(120, result[0].TotalDuration);
        }

        [Fact]
        public async Task GetMonthlyProgressAsync_InvalidUser_ShouldReturnEmptyList()
        {
            int userId = 99; 
            int year = 2024;
            int month = 1;
            _setup.SetupGetMonthlyProgress(userId, year, month, new List<WeeklyProgress>());

            var result = await _setup.WorkoutService.GetMonthlyProgressAsync(userId, year, month);

            Assert.NotNull(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task UpdateWorkoutAsync_ValidWorkout_ShouldUpdateWorkout()
        {
            var workout = new Workout
            {
                Id = 1,
                UserId = 1,
                ExerciseTypeId = 1,
                Duration = 60,
                Notes = "Updated notes",
                DateTime = DateTime.UtcNow
            };

            _setup.SetupUpdateWorkout(workout, workout);

            var result = await _setup.WorkoutService.UpdateWorkoutAsync(workout);

            Assert.NotNull(result);
            Assert.Equal("Updated notes", result?.Notes);
            _setup.WorkoutRepositoryMock.Verify(repo => repo.UpdateAsync(workout), Times.Once);
        }

        [Fact]
        public async Task UpdateWorkoutAsync_NonExistentWorkout_ShouldThrowException()
        {
            var workout = new Workout
            {
                Id = 99, 
                UserId = 1,
                ExerciseTypeId = 1,
                Duration = 60,
                Notes = "Non-existent workout",
                DateTime = DateTime.UtcNow
            };
            _setup.SetupUpdateWorkout(workout, null, throwException: true);

            var exception = await Assert.ThrowsAsync<Exception>(() => _setup.WorkoutService.UpdateWorkoutAsync(workout));
            Assert.Equal("Workout does not exist!", exception.Message);
        }

    }
}
