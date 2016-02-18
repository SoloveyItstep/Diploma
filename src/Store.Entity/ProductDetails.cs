using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class ProductDetails
    {
        public Int32 ProductDetailsID { get; set; }

        [StringLength(maximumLength: 200)]
        public String Value { get; set; }

        [StringLength(maximumLength: 15)]
        public String Measure { get; set; }
        public String Other { get; set; }

        public Int32 AppleID { get; set; }

        [JsonIgnore]
        public virtual Apple Apple { get; set; }

        public virtual DetailNames DetailNames { get; set; }
    }
}
