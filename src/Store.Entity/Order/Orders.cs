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
        [StringLength(10)]
        public String Date { get; set; }
        [StringLength(8)]
        public String OrderNumber { get; set; }
        [StringLength(36)]
        public String UserID { get; set; }
        public Decimal Sum { get; set; }
        [StringLength(12)]
        public String Status { get; set; }
        [StringLength(12)]
        public String Delivary { get; set; }
        [StringLength(15)]
        public String Payment { get; set; }

        public virtual ICollection<AppleOrders> AppleOrders { get; set; }
        
    }
}
