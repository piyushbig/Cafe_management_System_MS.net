using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace RolesAuth.Models
{
    

    
    
        public class BillEntity
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public double TotalAmount { get; set; }

            [ForeignKey("Order")]
            public int OrderId { get; set; }
            
            public OrderEntity Order { get; set; }
        }
    
}
