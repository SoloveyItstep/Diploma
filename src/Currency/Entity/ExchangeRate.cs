using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Entity
{
    public class ExchangeRate
    {
        public string baseCurrency { get; set; }
        public string currency { get; set; }
        public decimal saleRateNB { get; set; }
        public decimal purchaseRateNB { get; set; }
        public decimal saleRate { get; set; }
        public decimal purchaseRate { get; set; }
    }
}
