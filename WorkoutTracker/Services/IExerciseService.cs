using  WorkoutTracker.DTOs;
namespace WorkoutTracker.Services
{
    public interface IExerciseService
    {
        Task<List<ExerciseResponseDto>> GetAllAsync(string? MuscleGroup = null);
        Task <ExerciseResponseDto> GetByIdAsync(int id);
        Task<ExerciseResponseDto> CreateAsync(CreateExerciseDto dto);
        Task<bool> UpdateAsync(int id, CreateExerciseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
