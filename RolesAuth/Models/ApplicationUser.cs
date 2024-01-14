using Microsoft.AspNetCore.Identity;

namespace RolesAuth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public CafeEntity CafeDetails { get; set; }

        public CustomerEntity CustomerDetails { get; set; }
    }
}
