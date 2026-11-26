using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.DTOs;
using WorkoutTracker.Models;
using WorkoutTracker.Services;

namespace WorkoutTracker.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly AppDbContext _context;
        public WorkoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<WorkoutResponseDto>> GetAllAsync()
        {
            var workouts = await _context.Workouts
            .Include(w => w.Exercises)
            .ToListAsync();

            return workouts.Select(MapToDto).ToList();
        }

        public async Task<WorkoutResponseDto?> GetByIdAsync(int id)
        {
            var workout = await _context.Workouts
            .Include(w => w.Exercises)
            .FirstOrDefaultAsync(w => w.Id == id);

            return workout == null ? null : MapToDto(workout);
        }
        public async Task<WorkoutResponseDto> CreateAsync(CreateWorkoutDto dto)
        {
            var workout = new Workout
            {
                Title = dto.Title,
                Date = dto.Date,
                Notes = dto.Notes
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return MapToDto(workout);

        }
        public async Task<bool> UpdateAsync(int id, CreateWorkoutDto dto)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return false;
            }
            workout.Title = dto.Title;
            workout.Date = dto.Date;
            workout.Notes = dto.Notes;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return false;
            }
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return true;
        }
        private static WorkoutResponseDto MapToDto(Workout workout)
        {
            var exerciseDtos = workout.Exercises.Select(e => new ExerciseResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
                WeightKg = e.WeightKg,
                Reps = e.Reps,



            }).ToList();

            return new WorkoutResponseDto
            {
                Id = workout.Id,
                Title = workout.Title,
                Date = workout.Date,
                Notes = workout.Notes,
                Exercises = exerciseDtos,
                TotalVolumeKg = exerciseDtos.Sum(e => e.VolumeKg)
            };


        }
    }
}