using HMS.Entites;

namespace HMS.Models
{
    public class ExamineIndexViewModel
    {
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
        public Examination Examination { get; set; } = new Examination();
    }
}
