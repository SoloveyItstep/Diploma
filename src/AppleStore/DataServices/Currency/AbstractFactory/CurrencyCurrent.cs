using AppleStore.DataServices.Currency.Interfaces;
using Currency;
using Store.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.AbstractFactory
{
    public class CurrencyCurrent : ICurrencyCurrent
    {
        private ICurrencyUSD currency;
        public CurrencyCurrent(ICurrencyUSD currency)
        {
            this.currency = currency;
        }
        public string GetCurrentDateCurrency()
        {
            return currency.GetCurrentDateCurrency();
        }
    }
}
