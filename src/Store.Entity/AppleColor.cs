using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class AppleColor
    {
        public Int32 AppleColorID { get; set; }

        public Int32 AppleID { get; set; }
        [JsonIgnore]
        public virtual Apple Apple { get; set; }

        public Int32 ColorID { get; set; }
        public virtual Color Color { get; set; }

        public Int32 Count { get; set; }
    }
}
