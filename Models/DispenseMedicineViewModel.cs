using HMS.Entites;

namespace HMS.Models
{
    public class DispenseMedicineViewModel
    {
        public Patient? Patient { get; set; }
        public int ExaminationId { get; set; }
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
        public List<PharmacyInventory> Inventory { get; set; } = new List<PharmacyInventory>();
    }

}
