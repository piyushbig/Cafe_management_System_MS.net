using System.ComponentModel.DataAnnotations;

namespace RolesAuth.Models
{
    public class CartItems
    {
        [Key]
        public int CartFood_id { get; set; }
        public int ProductId { get; set; }

        // Rename the property to match the navigation property
        public int CustomerId { get; set; }

        public string CartFood_name { get; set; }
        public string Cafe_name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public CustomerEntity Customer { get; set; }
        public ProductEntity Product { get; set; }
    }

}
