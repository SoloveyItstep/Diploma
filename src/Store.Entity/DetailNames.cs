using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class DetailNames
    {
        public Int32 DetailNamesID { get; set; }

        [StringLength(maximumLength: 30)]
        public String Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductDetails> ProductDetails { get; set; }
    }
}
