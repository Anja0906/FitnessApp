using AutoMapper;
using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly IExerciseTypeService _exerciseTypeService;
        private readonly IMapper _mapper;

        public ExerciseTypeController(IExerciseTypeService exerciseTypeService, IMapper mapper)
        {
            _exerciseTypeService = exerciseTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExerciseTypes()
        {
            var exerciseTypes = await _exerciseTypeService.GetAllExerciseTypesAsync();
            var response = _mapper.Map<List<ExerciseTypeResponseDto>>(exerciseTypes);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseType(int id)
        {
            var exerciseType = await _exerciseTypeService.GetExerciseTypeByIdAsync(id);
            if (exerciseType == null) return NotFound("Exercise type not found");
            var response = _mapper.Map<ExerciseTypeResponseDto>(exerciseType);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseType([FromBody] ExerciseTypeRequestDto exerciseTypeDto)
        {
            var request = _mapper.Map<ExerciseType>(exerciseTypeDto);
            var newExerciseType = await _exerciseTypeService.AddExerciseTypeAsync(request);
            var response = _mapper.Map<ExerciseTypeResponseDto>(newExerciseType);
            return CreatedAtAction(nameof(GetExerciseType), new { id = response?.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExerciseType(int id, [FromBody] ExerciseTypeRequestDto exerciseTypeDto)
        {
            var request = _mapper.Map<ExerciseType>(exerciseTypeDto);
            var updatedExerciseType = await _exerciseTypeService.UpdateExerciseTypeAsync(request);
            if (updatedExerciseType == null) return NotFound("Exercise type not found");
            var response = _mapper.Map<ExerciseTypeResponseDto>(updatedExerciseType);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExerciseType(int id)
        {
            await _exerciseTypeService.DeleteExerciseTypeAsync(id);
            return NoContent();
        }
    }
}