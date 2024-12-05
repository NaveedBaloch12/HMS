using HMS.Entites;

namespace HMS.Entites
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }
        public User? Doctor { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
    }
}
