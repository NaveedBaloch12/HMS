namespace HMS.Entites
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Medicine name
        public string? Timing { get; set; } // e.g., Morning, Evening
        public int Days { get; set; } // Number of days the medicine should be taken
        public int ExaminationId { get; set; } // Foreign key to Examination

        public int Quantity { get; set; }
        public int Price { get; set; }

        // Navigation Property
        public Examination? Examination { get; set; }
    }

}
