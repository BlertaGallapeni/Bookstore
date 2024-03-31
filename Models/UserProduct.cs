using System;

namespace WebApp.Models
{
    public class UserProduct
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
