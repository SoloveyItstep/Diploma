using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity.Order
{
    public class AppleOrders
    {
        public Int32 AppleOrdersID { get; set; }
        public Int32 AppleID { get; set; }
        public virtual Apple Apple { get; set; }
        public Int32 OrdersID { get; set; }
        [JsonIgnore]
        public virtual Orders Orders { get; set; }
        public Int32 Count { get; set; }
    }
}
