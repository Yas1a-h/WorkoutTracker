using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.DTOs
{
    public class CreateWorkoutDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Title { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(500, ErrorMessage = "Notes must not exceed 500 characters")]
        public string? Notes { get; set; }
    }

    public class WorkoutResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public double TotalVolumeKg { get; set; }
        public List<ExerciseResponseDto> Exercises { get; set; } = new();
    }
}