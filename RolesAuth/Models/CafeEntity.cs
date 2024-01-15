using System.ComponentModel.DataAnnotations;

namespace RolesAuth.Models
{
    public class CafeEntity
    {

        [Key]
        public int CafeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        

        public string Address { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ProductEntity> products { get; set; }
    }
}
