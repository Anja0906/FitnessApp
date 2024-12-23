using FitnessApp.Application.Services;
using FitnessApp.Domain.Interfaces;
using FitnessApp.Domain.Model;
using Moq;

namespace FitnessApp.Tests.Application
{
    public class WorkoutServiceTestSetup
    {
        public Mock<IWorkoutRepository> WorkoutRepositoryMock { get; private set; }
        public WorkoutService WorkoutService { get; private set; }

        public WorkoutServiceTestSetup()
        {
            WorkoutRepositoryMock = new Mock<IWorkoutRepository>();
            WorkoutService = new WorkoutService(WorkoutRepositoryMock.Object);
        }

        public void SetupAddWorkout(Workout workout, Workout response)
        {
            if (workout == null)
            {
                WorkoutRepositoryMock.Setup(repo => repo.AddAsync(null))
                    .ThrowsAsync(new ArgumentNullException(nameof(workout), "Workout cannot be null."));
            }
            else
            {
                WorkoutRepositoryMock.Setup(repo => repo.AddAsync(workout)).ReturnsAsync(response);
            }
        }

        public void SetupDeleteWorkout(int workoutId)
        {
            WorkoutRepositoryMock.Setup(repo => repo.DeleteAsync(workoutId)).Returns(Task.CompletedTask);
        }

        public void SetupGetByUserId(int userId, List<Workout> workouts)
        {
            WorkoutRepositoryMock.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync(workouts);
        }

        public void SetupGetByDateRange(int userId, DateTime startDate, DateTime endDate, List<Workout> workouts)
        {
            WorkoutRepositoryMock.Setup(repo => repo.GetByDateRangeAsync(userId, startDate, endDate)).ReturnsAsync(workouts);
        }

        public void SetupGetMonthlyProgress(int userId, int year, int month, List<WeeklyProgress> progress)
        {
            WorkoutRepositoryMock.Setup(repo => repo.GetMonthlyProgressAsync(userId, year, month)).ReturnsAsync(progress);
        }

        public void SetupUpdateWorkout(Workout workout, Workout? response = null, bool throwException = false)
        {
            if (throwException)
            {
                WorkoutRepositoryMock.Setup(repo => repo.UpdateAsync(workout))
                    .ThrowsAsync(new Exception("Workout does not exist!"));
            }
            else
            {
                WorkoutRepositoryMock.Setup(repo => repo.UpdateAsync(workout)).ReturnsAsync(response);
            }
        }

    }
}
