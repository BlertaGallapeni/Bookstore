using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }
        public bool IsFavorite { get; set; }

    }
}
