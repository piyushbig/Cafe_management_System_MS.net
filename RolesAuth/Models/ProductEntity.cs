using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolesAuth.Models
{
    public class ProductEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Prize { get; set; }

        public string ImageUrl { get; set; }
        //oneToMany RelationShip with category
        [Required(ErrorMessage = "Please choose Image")]
        [NotMapped]
        public IFormFile Image { get; set; }

        public int CategoryId { get; set; } //forign key

        public CategoriesEntity Category { get; set; }

        public int CafeId { get; set; }
        public CafeEntity Cafe { get; set; }
    }
}
