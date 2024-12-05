using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Entites
{
    public class DispensedMedicine
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ExaminationId { get; set; } 
        public int Quantity { get; set; } 
        public decimal Cost { get; set; }
        public DateTime DispensedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; private set; }

        // Relationships
        public Patient? Patient { get; set; } 
        public Examination? Examination { get; set; }
        public PharmacyInventory? PharmacyInventory { get; set; } 
    }

}
