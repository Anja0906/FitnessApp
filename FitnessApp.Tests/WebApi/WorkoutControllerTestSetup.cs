using AutoMapper;
using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.Controllers;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FitnessApp.Tests.WebApi
{
    public class WorkoutControllerTestSetup
    {
        public Mock<IWorkoutService> WorkoutServiceMock { get; private set; }
        public Mock<IMapper> MapperMock { get; private set; }
        public WorkoutController WorkoutController { get; private set; }
        public DefaultHttpContext HttpContext { get; private set; }

        public WorkoutControllerTestSetup()
        {
            WorkoutServiceMock = new Mock<IWorkoutService>();
            MapperMock = new Mock<IMapper>();
            WorkoutController = new WorkoutController(WorkoutServiceMock.Object, MapperMock.Object);
            HttpContext = new DefaultHttpContext();
            WorkoutController.ControllerContext = new ControllerContext
            {
                HttpContext = HttpContext
            };
        }

        public void SetUserId(string userId)
        {
            HttpContext.Items["UserId"] = userId;
        }

        public void SetupGetWorkoutsByUser(int userId, List<Workout> workouts, List<WorkoutResponseDto> responseDtos)
        {
            WorkoutServiceMock.Setup(service => service.GetWorkoutByUserIdAsync(userId)).ReturnsAsync(workouts);
            MapperMock.Setup(mapper => mapper.Map<List<WorkoutResponseDto>>(workouts)).Returns(responseDtos);
        }

        public void SetupGetWorkoutsByDateRange(int userId, DateTime startDate, DateTime endDate, List<Workout> workouts, List<WorkoutResponseDto> responseDtos)
        {
            WorkoutServiceMock.Setup(service => service.GetWorkoutByDateRangeAsync(userId, startDate, endDate)).ReturnsAsync(workouts);
            MapperMock.Setup(mapper => mapper.Map<List<WorkoutResponseDto>>(workouts)).Returns(responseDtos);
        }

        public void SetupAddWorkout(Workout workout, WorkoutResponseDto responseDto)
        {
            if (workout == null)
            {
                WorkoutServiceMock.Setup(service => service.AddWorkoutAsync(null))
                    .ThrowsAsync(new ArgumentNullException(nameof(workout), "Workout cannot be null."));
            }
            else
            {
                WorkoutServiceMock.Setup(service => service.AddWorkoutAsync(workout)).ReturnsAsync(workout);
                MapperMock.Setup(mapper => mapper.Map<WorkoutResponseDto>(workout)).Returns(responseDto);
            }
        }

        public void SetupMapperForAddWorkout(WorkoutRequestDto requestDto, Workout mappedWorkout, WorkoutResponseDto responseDto)
        {
            MapperMock.Setup(mapper => mapper.Map<Workout>(requestDto)).Returns(mappedWorkout);
            MapperMock.Setup(mapper => mapper.Map<WorkoutResponseDto>(mappedWorkout)).Returns(responseDto);
        }


        public void SetupUpdateWorkout(Workout workout, WorkoutResponseDto responseDto)
        {
            WorkoutServiceMock.Setup(service => service.UpdateWorkoutAsync(workout)).ReturnsAsync(workout);
            MapperMock.Setup(mapper => mapper.Map<WorkoutResponseDto>(workout)).Returns(responseDto);
        }

        public void SetupMapperForUpdateWorkout(WorkoutUpdateRequestDto requestDto, Workout mappedWorkout, WorkoutResponseDto responseDto)
        {
            MapperMock.Setup(mapper => mapper.Map<Workout>(requestDto)).Returns(mappedWorkout);
            MapperMock.Setup(mapper => mapper.Map<WorkoutResponseDto>(mappedWorkout)).Returns(responseDto);
        }


        public void SetupDeleteWorkout(int workoutId, int userId)
        {
            WorkoutServiceMock.Setup(service => service.DeleteWorkoutAsync(workoutId, userId)).Returns(Task.CompletedTask);

            WorkoutServiceMock.Setup(service => service.DeleteWorkoutAsync(It.IsAny<int>(), It.Is<int>(id => id != userId)))
                .ThrowsAsync(new UnauthorizedAccessException("You are not authorized to delete this workout."));
        }


        public void SetupGetMonthlyProgress(int userId, int year, int month, List<WeeklyProgress> progress, List<WeeklyProgressResponseDto> responseDtos)
        {
            WorkoutServiceMock.Setup(service => service.GetMonthlyProgressAsync(userId, year, month)).ReturnsAsync(progress);
            MapperMock.Setup(mapper => mapper.Map<List<WeeklyProgressResponseDto>>(progress)).Returns(responseDtos);
        }

    }
}
