using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity.Order
{
    public class Orders
    {
        public Int32 OrdersID { get; set; }

        [Required]
        [StringLength(20)]
        public String Date { get; set; }

        [Required]
        [StringLength(8)]
        public String OrderNumber { get; set; }

        [StringLength(36)]
        [Required]
        public String UserID { get; set; }

        [Required]
        public Decimal Sum { get; set; }

        [StringLength(12)]
        [Required]
        public String Status { get; set; }

        [StringLength(12)]
        public String Delivery { get; set; }

        [StringLength(15)]
        public String Payment { get; set; }

        public virtual ICollection<AppleOrders> AppleOrders { get; set; }
        
    }
}
