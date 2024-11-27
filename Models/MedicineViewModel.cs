using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class MedicineViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Medicine Name")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage ="Enter Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public float? Price { get; set; }
    }
}
