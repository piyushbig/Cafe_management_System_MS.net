using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Models;

namespace RolesAuth.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CafeEntity> Cafes { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<CategoriesEntity> Categories { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.CafeDetails)
                .WithOne(c => c.User)
                .HasForeignKey<CafeEntity>(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); // Use DeleteBehavior.Cascade for automatic deletion
        }

        public DbSet<RolesAuth.Models.CustomerEntity>? CustomerEntity { get; set; }
    }
}
