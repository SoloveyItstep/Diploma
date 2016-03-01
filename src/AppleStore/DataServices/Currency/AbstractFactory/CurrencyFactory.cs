using AppleStore.DataServices.Currency.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.AbstractFactory
{
    public class CurrencyFactory : ICurrencyFactory
    {
        public CurrencyFactory(ICurrencyCurrent current, ICurrencyLast last)
        {
            this.current = current;
            this.last = last;
        }

        public ICurrencyCurrent current { get; set; }

        public ICurrencyLast last { get; set; }
    }
}
