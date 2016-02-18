using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class Color
    {
        public Int32 ColorID { get; set; }

        [StringLength(maximumLength: 20)]
        public String ColorName { get; set; }

        [JsonIgnore]
        public virtual ICollection<AppleColor> AppleColor { get; set; }
    }
}
