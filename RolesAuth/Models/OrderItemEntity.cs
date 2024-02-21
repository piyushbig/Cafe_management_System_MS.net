using RolesAuth.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace RolesAuth.Models
{
    public class OrderItemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public int ProductId { get; set; }


        public int Quantity { get; set; }
        public double Subtotal { get; set; }







        public ProductEntity Product { get; set; }
        public OrderEntity Order { get; set; }
    }
}
