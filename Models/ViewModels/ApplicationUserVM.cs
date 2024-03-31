using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ApplicationUserVM
    {
        public string? Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public char Gender { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }

    }
}
