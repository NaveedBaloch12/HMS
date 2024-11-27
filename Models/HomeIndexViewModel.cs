using HMS.Entites;

namespace HMS.Models
{
    public class HomeIndexViewModel
    {
        public List<Appointment>? Appointments { get; set; }
        public List<Patient>? Patients { get; set; }
        public User? User { get; set; }
        public List<Medicine>? Medicines { get; set; }

    }
}
