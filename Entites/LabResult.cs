namespace HMS.Entites
{
    public class LabResult
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorSuggestionId { get; set; }
        public string? TestName { get; set; }
        public string? Result { get; set; }
        public DateTime PerformedDate { get; set; }

        public Patient? Patient { get; set; }
        public DoctorSuggestion? DoctorSuggestion { get; set; }
    }

}
