namespace HMS.Entites
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int Days { get; set; } 
        public int Timing { get; set; }
        public int ExaminationId { get; set; } 
        // Navigation Property
        public Examination? Examination { get; set; }
    }

}
