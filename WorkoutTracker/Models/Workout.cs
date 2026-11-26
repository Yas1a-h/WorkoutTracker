namespace WorkoutTracker.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        public List<Exercise> Exercises { get; set; } = new();
    }
}
