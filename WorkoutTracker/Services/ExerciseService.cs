using WorkoutTracker.DTOs;
using WorkoutTracker.Models;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
namespace WorkoutTracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly AppDbContext _context;

        public ExerciseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ExerciseResponseDto>> GetAllAsync(string? muscleGroup = null)
        {
            var query = _context.Exercises.AsQueryable();
            if(!string.IsNullOrWhiteSpace(muscleGroup))
            {
                query = query.Where(e => e.MuscleGroup.ToLower() == muscleGroup.ToLower());
            }
            var exercise = await query.ToListAsync();
            return exercise.Select(MapToDto).ToList();
        }
        public async Task<ExerciseResponseDto?> GetByIdAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            return exercise == null ? null : MapToDto(exercise);
        }
        public async Task<ExerciseResponseDto> CreateAsync(CreateExerciseDto dto)
        {
            // Check if the associated workout exists
            var WorkoutExists = await _context.Workouts.AnyAsync(w => w.Id == dto.WorkoutId);
            if (!WorkoutExists)
            {
                throw new ArgumentException($"Workout with ID {dto.WorkoutId} does not exist.");
            }
            var exercise = new Exercise
            {
                Name = dto.Name,
                MuscleGroup = dto.MuscleGroup,
                WeightKg = dto.WeightKg,
                Reps = dto.Reps,
                WorkoutId = dto.WorkoutId

            };
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return MapToDto(exercise);
        }
        public async Task<bool> UpdateAsync(int id, CreateExerciseDto dto)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return false;
            }
            exercise.Name = dto.Name;
            exercise.MuscleGroup = dto.MuscleGroup;
            exercise.WeightKg = dto.WeightKg;
            exercise.Reps = dto.Reps;
            exercise.WorkoutId = dto.WorkoutId;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync (int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return false;
            }
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return true;
        }
        private static ExerciseResponseDto MapToDto(Exercise exercise)
        {
            return new ExerciseResponseDto
            {
                Id = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                WeightKg = exercise.WeightKg,
                Reps = exercise.Reps
            };
        }





    }
}
