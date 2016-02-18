using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class Image
    {
        public Int32 ImageID { get; set; }

        [StringLength(maximumLength: 150)]
        public String Path { get; set; }

        [StringLength(maximumLength: 6)]
        public String Size { get; set; }
        public Int32 ColorID { get; set; }

        public Int32 AppleID { get; set; }
        [JsonIgnore]
        public virtual Apple Apple { get; set; }
    }
}
