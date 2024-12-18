using AutoMapper;
using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.WebApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWorkoutsByUser(int userId)
        {
            var workouts = await _workoutService.GetWorkoutByUserIdAsync(userId);
            var workoutResponses = _mapper.Map<List<WorkoutResponseDto>>(workouts);
            return Ok(workoutResponses);
        }

        [HttpGet("user/{userId}/date-range")]
        public async Task<IActionResult> GetWorkoutsByDateRange(int userId, DateTime startDate, DateTime endDate)
        {
            var workouts = await _workoutService.GetWorkoutByDateRangeAsync(userId, startDate, endDate);
            var workoutResponses = _mapper.Map<List<WorkoutResponseDto>>(workouts);
            return Ok(workoutResponses);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout([FromBody] WorkoutRequestDto workoutRequestDto)
        {
            var workout = _mapper.Map<Workout>(workoutRequestDto);
            var newWorkout = await _workoutService.AddWorkoutAsync(workout);
            var workoutResponse = _mapper.Map<WorkoutResponseDto>(newWorkout);
            return CreatedAtAction(nameof(GetWorkoutsByUser), new { userId = workout.UserId }, workoutResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] WorkoutRequestDto workoutRequestDto)
        {
            var workout = _mapper.Map<Workout>(workoutRequestDto);
            workout.Id = id;
            var updatedWorkout = await _workoutService.UpdateWorkoutAsync(workout);
            if (updatedWorkout == null) return NotFound("Workout not found");
            var workoutResponse = _mapper.Map<WorkoutResponseDto>(updatedWorkout);
            return Ok(workoutResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            await _workoutService.DeleteWorkoutAsync(id);
            return NoContent();
        }
    }
}
