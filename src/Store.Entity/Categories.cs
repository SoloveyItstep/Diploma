using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class Categories
    {
        [Key]
        public Int32 CategoriesID { get; set; }

        [StringLength(maximumLength: 20)]
        public String CategoryName { get; set; }

        public Int32 AppleID;
        [JsonIgnore]
        public virtual ICollection<Apple> Apple { get; set; }
    }
}
