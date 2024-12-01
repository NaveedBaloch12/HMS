using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HMS.Entites
{
    public class Patient
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Contact { get; set; }
        public string? Address { get; set; }
        public string? MedicalIssue { get; set; }
        public string? MedicalHistory { get; set; }

        public ICollection<DispensedMedicine>? DispensedMedicines { get; set; }


        public int CalculateAge()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - DateOfBirth.Year;

            // Check if the birthday has not occurred yet this year
            if (today < DateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
