namespace HMS.Entites
{
    public class DispensedMedicine
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ExaminationId { get; set; } 
        public int MedicineId { get; set; } 
        public int Quantity { get; set; } 
        public DateTime DispensedDate { get; set; } 

        // Relationships
        public Patient? Patient { get; set; } 
        public Examination? Examination { get; set; }
        public PharmacyInventory? PharmacyInventory { get; set; } 
    }

}
