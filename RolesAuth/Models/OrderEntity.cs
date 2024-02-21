



using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolesAuth.Models
{
    public class OrderEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public Status OrderStatus { get; set; }
        public double TotalAmount { get; set; }

        public int CustomerId { get; set; }

        
        public CustomerEntity Customer { get; set; }

        public enum Status
        {
            DELIVERED,
            APPROVED,
            PENDING
        }

        public ICollection<OrderItemEntity> OrderItems { get; set; }

        public BillEntity Bill { get; set; }
    }
}
