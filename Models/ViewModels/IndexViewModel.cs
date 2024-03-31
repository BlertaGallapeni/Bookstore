using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class IndexViewModel
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
        public DateTime Birthday { get; set; }
        [Required]
        public char Gender { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string? StatusMessage { get; set; }
        public List<Product>? Products { get; set; }
    }
}
