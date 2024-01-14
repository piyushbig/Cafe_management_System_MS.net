using System.ComponentModel.DataAnnotations;

namespace RolesAuth.Models
{
    public class CategoriesEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //one to many relationShip with Product Model

        public ICollection<ProductEntity> products { get; set; }
    }
}
