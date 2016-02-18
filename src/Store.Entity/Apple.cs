using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class Apple
    {
        public Int32 AppleID { get; set; }

        [StringLength(maximumLength: 30)]
        public String Subcategory { get; set; }

        [StringLength(maximumLength: 15)]
        public String Construction { get; set; }

        [StringLength(maximumLength: 15)]
        public String Type { get; set; }

        [StringLength(maximumLength: 30)]
        public String Model { get; set; }

        [StringLength(maximumLength: 30)]
        public String Name { get; set; }
        public Decimal Price { get; set; }

        [StringLength(maximumLength: 100)]
        public String Url { get; set; }

        public virtual ICollection<AppleColor> AppleColor { get; set; }
        public virtual ICollection<Image> AppleImage { get; set; }
        public virtual ICollection<ProductDetails> ProductDetails { get; set; }
        
        public virtual Categories Categories { get; set; }
    }
}
