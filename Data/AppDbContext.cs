using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProduct>()
                .HasKey(up => new { up.UserId, up.ProductId });

            modelBuilder.Entity<UserProduct>()
                .HasOne<ApplicationUser>(sc => sc.ApplicationUser)
                .WithMany(s => s.UserProducts)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserProduct>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.UserProducts)
                .HasForeignKey(sc => sc.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
