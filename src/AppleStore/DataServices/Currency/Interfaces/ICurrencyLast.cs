using Currency.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.Interfaces
{
    public interface ICurrencyLast
    {
        void GetLastCurrencyFromPB();
        Task<Boolean> DateExist();
        void CreateCurrency();
        Decimal GetCurrency();
    }
}
