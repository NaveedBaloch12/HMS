using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class PharmacyInventoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Medicine Name")]
        public string? MedicineName { get; set; }

        [Required(ErrorMessage ="Enter Stock Quantity")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage ="Enter Price of Unit")]
        public decimal PricePerUnit { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
