using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ManageProfileVM
    {
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        List<Product> Products { get; set; } 
    }
}
