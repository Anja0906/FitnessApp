using AutoMapper;
using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using FitnessApp.WebApi.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.WebApi.Controllers
{
    [Route(WorkoutRoutes.Base)]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet(WorkoutRoutes.GetWorkoutsByUser)]
        public async Task<IActionResult> GetWorkoutsByUser(int userId)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != userId.ToString())
            {
                return Forbid();
            }

            var workouts = await _workoutService.GetWorkoutByUserIdAsync(userId);
            var workoutResponses = _mapper.Map<List<WorkoutResponseDto>>(workouts);
            return Ok(workoutResponses);
        }

        [Authorize]
        [HttpGet(WorkoutRoutes.GetWorkoutsByDateRange)]
        public async Task<IActionResult> GetWorkoutsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != userId.ToString())
            {
                return Forbid();
            }
            var workouts = await _workoutService.GetWorkoutByDateRangeAsync(userId, startDate, endDate);
            var workoutResponses = _mapper.Map<List<WorkoutResponseDto>>(workouts);
            return Ok(workoutResponses);
        }

        [Authorize]
        [HttpPost(WorkoutRoutes.AddWorkout)]
        public async Task<IActionResult> AddWorkout([FromBody] WorkoutRequestDto workoutRequestDto)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != workoutRequestDto.UserId.ToString())
            {
                return Forbid();
            }
            var workout = _mapper.Map<Workout>(workoutRequestDto);
            var newWorkout = await _workoutService.AddWorkoutAsync(workout);
            var workoutResponse = _mapper.Map<WorkoutResponseDto>(newWorkout);
            return CreatedAtAction(nameof(GetWorkoutsByUser), new { userId = workout.UserId }, workoutResponse);
        }

        [Authorize]
        [HttpPut(WorkoutRoutes.UpdateWorkout)]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] WorkoutUpdateRequestDto workoutRequestDto)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != workoutRequestDto.UserId.ToString())
            {
                return Forbid();
            }
            var workout = _mapper.Map<Workout>(workoutRequestDto);
            workout.Id = id;
            var updatedWorkout = await _workoutService.UpdateWorkoutAsync(workout);
            if (updatedWorkout == null) return NotFound("Workout not found");
            var workoutResponse = _mapper.Map<WorkoutResponseDto>(updatedWorkout);
            return Ok(workoutResponse);
        }

        [Authorize]
        [HttpDelete(WorkoutRoutes.DeleteWorkout)]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            await _workoutService.DeleteWorkoutAsync(id, Int32.Parse(loggedInUserId!));
            return NoContent();
        }
    }
}
