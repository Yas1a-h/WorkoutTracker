using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.DTOs
{
    public class CreateExerciseDto
    {
        [Required(ErrorMessage = "Exercise name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Muscle group is required.")]
        [StringLength(50, ErrorMessage = "Muscle group cannot exceed 50 characters.")]
        public string MuscleGroup { get; set; } = string.Empty;

        [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000 kg.")]
        public double WeightKg { get; set; }

        [Range(1, 500, ErrorMessage = "Reps must be between 1 and 500.")]
        public int Reps { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A valid Workout ID is required.")]
        public int WorkoutId { get; set; }
    }

    public class ExerciseResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
        public double WeightKg { get; set; }
        public int Reps { get; set; }
        public double VolumeKg => WeightKg * Reps;
    }
}