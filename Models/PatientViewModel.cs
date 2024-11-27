using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class PatientViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Patient Gender is Required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Patient DateOfBirth is Required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Patient Contact Number is Required")]
        public string? Contact { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Patient Mediacl Issue is Required")]
        public string? MedicalIssue { get; set; }
    }
}
