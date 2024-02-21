using System.ComponentModel.DataAnnotations;

namespace RolesAuth.Models
{
    public class CustomerEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public string Address { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<CartItems> CartItems { get; set; }
    }
}
