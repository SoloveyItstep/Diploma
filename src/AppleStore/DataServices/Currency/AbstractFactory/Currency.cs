using AppleStore.DataServices.Currency.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.AbstractFactory
{
    public class Currency : ICurrency
    {
        private ICurrencyFactory factory;
        public Currency(ICurrencyFactory factory)
        {
            this.factory = factory;
        }

        public async Task<Decimal> GetCurrency()
        {
            String uah = factory.current.GetCurrentDateCurrency();
            if(uah == null)
            {
                factory.last.GetLastCurrencyFromPB();
                if(!await factory.last.DateExist())
                {
                    factory.last.CreateCurrency();
                }
                return factory.last.GetCurrency();
            }
            if(uah == "false")
            {
                return -1;
            }
            return Decimal.Parse(uah);
        }
    }
}
