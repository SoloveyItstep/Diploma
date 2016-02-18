using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Entity
{
    public class UserServices
    {
        public DateTime date { get; set; }
        public string bank { get; set; }
        public int baseCurrency { get; set; }
        public string baseCurrencyLit { get; set; }

        public List<ExchangeRate> exchangeRate { get; set; }
    }
}
