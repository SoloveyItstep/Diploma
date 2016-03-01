using AppleStore.DataServices.Currency.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.AbstractFactory
{
    public class Currency : ICurrency
    {
        private ICurrencyCurrent current;
        private ICurrencyLast last;
        public Currency(ICurrencyFactory factory)
        {
            this.current = factory.current;
            this.last = factory.last;
        }
        public async Task<Decimal> GetCurrency()
        {
            String uah = current.GetCurrentDateCurrency();
            if(uah == null)
            {
                last.GetLastCurrencyFromPB();
                if(!await last.DateExist())
                {
                    last.CreateCurrency();
                }
                return last.GetCurrency();
            }
            return Decimal.Parse(uah);
        }
    }
}
