using WorkoutTracker.DTOs;
namespace WorkoutTracker.Services
{
    public interface IWorkoutService
    {
        Task<List<WorkoutResponseDto>> GetAllAsync();
        Task<WorkoutResponseDto?> GetByIdAsync(int id);
        Task<WorkoutResponseDto> CreateAsync(CreateWorkoutDto dto);
        Task<bool> UpdateAsync(int id, CreateWorkoutDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
