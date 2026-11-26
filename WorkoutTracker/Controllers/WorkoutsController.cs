using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.DTOs;
using WorkoutTracker.Services;

namespace WorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkoutResponseDto>>> GetAll()
        {
            return await _workoutService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutResponseDto>> GetById(int id)
        {
            var workout = await _workoutService.GetByIdAsync(id);
            if (workout == null) return NotFound("Workout not found.");
            return workout;
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutResponseDto>> Create(CreateWorkoutDto dto)
        {
            var created = await _workoutService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateWorkoutDto dto)
        {
            var updated = await _workoutService.UpdateAsync(id, dto);
            if (!updated) return NotFound("Workout not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _workoutService.DeleteAsync(id);
            if (!deleted) return NotFound("Workout not found.");
            return NoContent();
        }
    }
}