﻿using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? ContactNo { get; set; }
        [Required]
        public string? Role { get; set; }
        public int Sallary { get; set; }
    }
}
