using System.Numerics;

namespace HMS.Entites
{
    public class SuggestedTest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }
        public User? Doctor { get; set; }

        public string? TestName { get; set; }
        public string? Description { get; set; }
        public DateTime SuggestedDate { get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; }
    }

}
