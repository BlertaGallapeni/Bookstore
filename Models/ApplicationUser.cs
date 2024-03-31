using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
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
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool? Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }

    }
}
