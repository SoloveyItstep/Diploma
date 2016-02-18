using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Entity
{
    public class Currency
    {
        [Key]
        public Int32 CurrencyID { get; set; }
        [StringLength(10)]
        public String Date { get; set; }
        [StringLength(20)]
        public String CurrencyUSD { get; set; }
    }
}