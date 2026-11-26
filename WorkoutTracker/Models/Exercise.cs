using System.Text.Json.Serialization;


namespace WorkoutTracker.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
        public double WeightKg { get; set; }
        public int Reps { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;


        public int WorkoutId { get; set; }
        [JsonIgnore]
        public Workout? Workout { get; set; }
    }
}
