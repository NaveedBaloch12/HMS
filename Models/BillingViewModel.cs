using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{

    public class BillingViewModel
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string? PatientName { get; set; }

        public decimal DoctorFee { get; set; }
        public decimal MedicineCost { get; set; }
        public decimal TestCost { get; set; }

        public decimal TotalAmount => DoctorFee + MedicineCost + TestCost;
    }

}
