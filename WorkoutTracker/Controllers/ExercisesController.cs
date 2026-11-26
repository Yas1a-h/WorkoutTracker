using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;


namespace WorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExercisesController(AppDbContext context)
        {
            _context = context;
        }
        // Read all exercises
        [HttpGet]
        public async Task<ActionResult<List<Exercise>>> GetAll()
        {
            return await _context.Exercises.ToListAsync();
        }

        // Read by ID
        [HttpGet("{id}")]

        public async Task<ActionResult<Exercise>> GetById(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound("Exercise not found");
            }
            return exercise;

        }
        // Add a new exercise
        [HttpPost]

        public async Task<ActionResult<Exercise>> Create(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = exercise.Id }, exercise);
        }
        //UPDATE an existing exercise
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Exercise updatedExercise)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound("Exercise not found");
            }
            exercise.Name = updatedExercise.Name;
            exercise.MuscleGroup = updatedExercise.MuscleGroup;
            exercise.WeightKg = updatedExercise.WeightKg;
            exercise.Reps = updatedExercise.Reps;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        //Delete an exercise
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null )
            {
                return NotFound("Exercise not found");
            }
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }
}
