using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FitnessApp.Tests.WebApi
{
    public class WorkoutControllerTests
    {
        private readonly WorkoutControllerTestSetup _setup;

        public WorkoutControllerTests()
        {
            _setup = new WorkoutControllerTestSetup();
        }

        [Fact]
        public async Task GetWorkoutsByUser_AuthorizedUser_ReturnsWorkouts()
        {
            int userId = 1;
            _setup.SetUserId(userId.ToString());

            var workouts = new List<Workout>
            {
                new Workout { Id = 1, UserId = userId, ExerciseTypeId = 1, Duration = 60 },
                new Workout { Id = 2, UserId = userId, ExerciseTypeId = 2, Duration = 45 }
            };

            var responseDtos = new List<WorkoutResponseDto>
            {
                new WorkoutResponseDto { Id = 1, UserId = userId, ExerciseTypeId = 1, Duration = 60 },
                new WorkoutResponseDto { Id = 2, UserId = userId, ExerciseTypeId = 2, Duration = 45 }
            };

            _setup.SetupGetWorkoutsByUser(userId, workouts, responseDtos);

            var result = await _setup.WorkoutController.GetWorkoutsByUser(userId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WorkoutResponseDto>;
            Assert.Equal(responseDtos.Count, responseData.Count);
        }

        [Fact]
        public async Task GetWorkoutsByUser_UnauthorizedUser_ReturnsForbid()
        {
            int userId = 1;
            _setup.SetUserId("2");

            var result = await _setup.WorkoutController.GetWorkoutsByUser(userId);

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task GetWorkoutsByUser_NoLoggedInUser_ReturnsForbid()
        {
            int userId = 1000;
            _setup.SetUserId(null);

            var result = await _setup.WorkoutController.GetWorkoutsByUser(userId);

            Assert.IsType<ForbidResult>(result);
        }


        [Fact]
        public async Task GetWorkoutsByUser_AuthorizedUser_ReturnsEmptyList()
        {
            int userId = 1;
            _setup.SetUserId(userId.ToString());

            var workouts = new List<Workout>();
            var responseDtos = new List<WorkoutResponseDto>(); 

            _setup.SetupGetWorkoutsByUser(userId, workouts, responseDtos);

            var result = await _setup.WorkoutController.GetWorkoutsByUser(userId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WorkoutResponseDto>;
            Assert.NotNull(responseData);
            Assert.Empty(responseData); 
        }



        [Fact]
        public async Task AddWorkout_UnauthorizedUser_ReturnsForbid()
        {
            var workoutRequestDto = new WorkoutRequestDto { UserId = 2, ExerciseTypeId = 1, Duration = 60 };
            _setup.SetUserId("1");

            var result = await _setup.WorkoutController.AddWorkout(workoutRequestDto);

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task AddWorkout_AuthorizedUser_ReturnsCreatedWorkout()
        {
            var workoutRequestDto = new WorkoutRequestDto { UserId = 1, ExerciseTypeId = 1, Duration = 60 };
            var workout = new Workout { UserId = 1, ExerciseTypeId = 1, Duration = 60 };
            var responseDto = new WorkoutResponseDto { Id = 1, UserId = 1, ExerciseTypeId = 1, Duration = 60 };

            _setup.SetUserId("1");
            _setup.SetupAddWorkout(workout, responseDto);
            _setup.SetupMapperForAddWorkout(workoutRequestDto, workout, responseDto);

            var result = await _setup.WorkoutController.AddWorkout(workoutRequestDto);

            Assert.IsType<CreatedAtActionResult>(result);
            var createdResult = result as CreatedAtActionResult;
            var responseData = createdResult.Value as WorkoutResponseDto;
            Assert.Equal(responseDto.Id, responseData.Id);
        }

        [Fact]
        public async Task AddWorkout_NullRequest_ThrowsException()
        {
            // Arrange
            WorkoutRequestDto workoutRequestDto = null; // Null input
            _setup.SetUserId("1");

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _setup.WorkoutController.AddWorkout(workoutRequestDto));
        }




        [Fact]
        public async Task GetWorkoutsByDateRange_AuthorizedUser_ReturnsWorkouts()
        {
            int userId = 1;
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 1, 31);
            _setup.SetUserId(userId.ToString());

            var workouts = new List<Workout>
            {
                new Workout { Id = 1, UserId = userId, ExerciseTypeId = 1, Duration = 30, DateTime = new DateTime(2024, 1, 10) },
                new Workout { Id = 2, UserId = userId, ExerciseTypeId = 2, Duration = 45, DateTime = new DateTime(2024, 1, 20) }
            };

            var responseDtos = new List<WorkoutResponseDto>
            {
                new WorkoutResponseDto { Id = 1, UserId = userId, ExerciseTypeId = 1, Duration = 30, DateTime = new DateTime(2024, 1, 10) },
                new WorkoutResponseDto { Id = 2, UserId = userId, ExerciseTypeId = 2, Duration = 45, DateTime = new DateTime(2024, 1, 20) }
            };

            _setup.SetupGetWorkoutsByDateRange(userId, startDate, endDate, workouts, responseDtos);

            var result = await _setup.WorkoutController.GetWorkoutsByDateRange(userId, startDate, endDate);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WorkoutResponseDto>;
            Assert.Equal(responseDtos.Count, responseData.Count);
            Assert.Equal(responseDtos[0].Id, responseData[0].Id);
            Assert.Equal(responseDtos[1].Id, responseData[1].Id);
        }

        [Fact]
        public async Task GetWorkoutsByDateRange_UnauthorizedUser_ReturnsForbid()
        {
            int userId = 2;
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 1, 31);
            _setup.SetUserId("1"); 

            var result = await _setup.WorkoutController.GetWorkoutsByDateRange(userId, startDate, endDate);

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task GetWorkoutsByDateRange_NoWorkoutsFound_ReturnsEmptyList()
        {
            int userId = 1;
            DateTime startDate = new DateTime(2024, 2, 1);
            DateTime endDate = new DateTime(2024, 2, 28);
            _setup.SetUserId(userId.ToString());

            var workouts = new List<Workout>();
            var responseDtos = new List<WorkoutResponseDto>();

            _setup.SetupGetWorkoutsByDateRange(userId, startDate, endDate, workouts, responseDtos);

            var result = await _setup.WorkoutController.GetWorkoutsByDateRange(userId, startDate, endDate);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WorkoutResponseDto>;
            Assert.Empty(responseData);
        }

        [Fact]
        public async Task UpdateWorkout_AuthorizedUser_ReturnsUpdatedWorkout()
        {
            int id = 1;
            var workoutUpdateRequestDto = new WorkoutUpdateRequestDto { UserId = 1, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };
            var workout = new Workout { Id = id, UserId = 1, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };
            var responseDto = new WorkoutResponseDto { Id = id, UserId = 1, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };

            _setup.SetUserId("1");
            _setup.SetupMapperForUpdateWorkout(workoutUpdateRequestDto, workout, responseDto);
            _setup.SetupUpdateWorkout(workout, responseDto);

            var result = await _setup.WorkoutController.UpdateWorkout(id, workoutUpdateRequestDto);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as WorkoutResponseDto;
            Assert.Equal(responseDto.Id, responseData.Id);
            Assert.Equal(responseDto.Notes, responseData.Notes);
        }

        [Fact]
        public async Task UpdateWorkout_WorkoutNotFound_ReturnsNotFound()
        {
            int id = 1;
            var workoutUpdateRequestDto = new WorkoutUpdateRequestDto { UserId = 1, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };
            var workout = new Workout { Id = id, UserId = 1, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };

            _setup.SetUserId("1");
            _setup.MapperMock.Setup(mapper => mapper.Map<Workout>(workoutUpdateRequestDto)).Returns(workout);
            _setup.WorkoutServiceMock.Setup(service => service.UpdateWorkoutAsync(workout)).ReturnsAsync((Workout)null);

            var result = await _setup.WorkoutController.UpdateWorkout(id, workoutUpdateRequestDto);

            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal("Workout not found", notFoundResult.Value);
        }

        [Fact]
        public async Task UpdateWorkout_UnauthorizedUser_ReturnsForbid()
        {
            int id = 1;
            var workoutUpdateRequestDto = new WorkoutUpdateRequestDto { UserId = 2, ExerciseTypeId = 1, Duration = 60, Notes = "Updated notes" };

            _setup.SetUserId("1");

            var result = await _setup.WorkoutController.UpdateWorkout(id, workoutUpdateRequestDto);

            Assert.IsType<ForbidResult>(result);
        }


        [Fact]
        public async Task DeleteWorkout_AuthorizedUser_ReturnsNoContent()
        {
            int workoutId = 1;
            _setup.SetUserId("1");
            _setup.SetupDeleteWorkout(workoutId, 1);

            var result = await _setup.WorkoutController.DeleteWorkout(workoutId);

            Assert.IsType<NoContentResult>(result);
            _setup.WorkoutServiceMock.Verify(service => service.DeleteWorkoutAsync(workoutId, 1), Times.Once);
        }

        [Fact]
        public async Task DeleteWorkout_WorkoutNotFound_ThrowsException()
        {
            int workoutId = 1;
            _setup.SetUserId("1");
            _setup.WorkoutServiceMock.Setup(service => service.DeleteWorkoutAsync(workoutId, 1))
                .ThrowsAsync(new Exception("Workout not found"));

            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _setup.WorkoutController.DeleteWorkout(workoutId));

            Assert.Equal("Workout not found", exception.Message);
            _setup.WorkoutServiceMock.Verify(service => service.DeleteWorkoutAsync(workoutId, 1), Times.Once);
        }

        [Fact]
        public async Task GetMonthlyProgress_AuthorizedUser_ReturnsProgress()
        {
            int userId = 1;
            int year = 2024;
            int month = 1;

            _setup.SetUserId(userId.ToString());

            var progress = new List<WeeklyProgress>
            {
                new WeeklyProgress { Week = 1, TotalDuration = 120, WorkoutCount = 2, AverageIntensity = 3.5, AverageFatigueLevel = 2.5 },
                new WeeklyProgress { Week = 2, TotalDuration = 90, WorkoutCount = 1, AverageIntensity = 4.0, AverageFatigueLevel = 3.0 }
            };

            var progressResponseDtos = new List<WeeklyProgressResponseDto>
            {
                new WeeklyProgressResponseDto { Week = 1, TotalDuration = 120, WorkoutCount = 2, AverageIntensity = 3.5, AverageFatigueLevel = 2.5 },
                new WeeklyProgressResponseDto { Week = 2, TotalDuration = 90, WorkoutCount = 1, AverageIntensity = 4.0, AverageFatigueLevel = 3.0 }
            };

            _setup.SetupGetMonthlyProgress(userId, year, month, progress, progressResponseDtos);

            var result = await _setup.WorkoutController.GetMonthlyProgress(userId, year, month);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WeeklyProgressResponseDto>;
            Assert.Equal(progressResponseDtos.Count, responseData.Count);
            Assert.Equal(progressResponseDtos[0].Week, responseData[0].Week);
        }

        [Fact]
        public async Task GetMonthlyProgress_NoData_ReturnsEmptyList()
        {
            int userId = 1;
            int year = 2024;
            int month = 2;

            _setup.SetUserId(userId.ToString());

            var progress = new List<WeeklyProgress>();
            var progressResponseDtos = new List<WeeklyProgressResponseDto>();

            _setup.SetupGetMonthlyProgress(userId, year, month, progress, progressResponseDtos);

            var result = await _setup.WorkoutController.GetMonthlyProgress(userId, year, month);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var responseData = okResult.Value as List<WeeklyProgressResponseDto>;
            Assert.NotNull(responseData);
            Assert.Empty(responseData);
        }

        [Fact]
        public async Task GetMonthlyProgress_UnauthorizedUser_ReturnsForbid()
        {
            int userId = 1;
            int year = 2024;
            int month = 1;

            _setup.SetUserId("2"); 
            var result = await _setup.WorkoutController.GetMonthlyProgress(userId, year, month);

            Assert.IsType<ForbidResult>(result);
        }

    }
}
