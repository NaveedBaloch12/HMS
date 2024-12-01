namespace HMS.Entites
{
    public class Examination
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public string? Diagnosis { get; set; }
        public string? Notes { get; set; }

        public List<Medicine>? Medicines { get; set; }

        public ICollection<DispensedMedicine>? DispensedMedicines { get; set; }
    }

}
