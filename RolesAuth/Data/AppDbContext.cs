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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
