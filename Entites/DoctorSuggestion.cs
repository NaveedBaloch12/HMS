using System.Numerics;

namespace HMS.Entites
{
    public class DoctorSuggestion
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string? TestName { get; set; }
        public DateTime SuggestedDate { get; set; }
        public bool IsCompleted { get; set; } // Indicates whether the test has been performed

        public Patient? Patient { get; set; }
        public User? Doctor { get; set; }
    }

}
