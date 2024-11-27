using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class LogInViewModel
    {        
        [Required(ErrorMessage = "Enter User Name")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Select User Role")]
        public string? Role { get; set; }
    }
}
