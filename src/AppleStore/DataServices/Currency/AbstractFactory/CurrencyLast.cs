using AppleStore.DataServices.Currency.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currency.Entity;
using Currency;
using Store.Repository.UnitOfWorks;

namespace AppleStore.DataServices.Currency.AbstractFactory
{
    public class CurrencyLast : ICurrencyLast
    {
        private Data data;
        private ICurrencyUSD currency;
        private IUnitOfWork unitOfWork;
        
        public CurrencyLast(ICurrencyUSD currency, IUnitOfWork unitOfWork)
        {
            this.currency = currency;
            this.unitOfWork = unitOfWork;
        }

        public async void CreateCurrency()
        {
            unitOfWork.Currency.Add(new Store.Entity.Currency()
            {
                CurrencyUSD = data.curency.ToString(),
                Date = data.date.ToShortDateString()
            });
            await unitOfWork.CommitAsync();
        }

        public async Task<Boolean> DateExist()
        {
            return await unitOfWork.Currency.DateExist(this.data.date);
        }

        public Decimal GetCurrency()
        {
            return Decimal.Parse(data.curency);
        }

        public void GetLastCurrencyFromPB()
        {
            data = currency.GetLastCurrency(DateTime.Now);
        }
    }
}
