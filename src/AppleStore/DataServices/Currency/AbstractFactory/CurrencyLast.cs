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

        public async Task CreateCurrency()
        {
            //var curr = await GetLastFromDB();
            var curr = unitOfWork.Currency.GetLast();
            //if (curr == null)
            //{
            //    var c = new Store.Entity.Currency()
            //    {
            //        CurrencyUSD = data.curency.ToString(),
            //        Date = data.date.ToShortDateString()
            //    };
            //    try {
            //        unitOfWork.Currency.Add(c);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //    await unitOfWork.CommitAsync();
            //}
            if (curr.Date != data.date.ToShortDateString())
            {
                //curr.CurrencyUSD = data.curency.ToString();
                //curr.Date = data.date.ToShortDateString();
                var currency = new Store.Entity.Currency()
                {
                    CurrencyUSD = data.curency,
                    Date = data.date.ToShortDateString()
                };
                unitOfWork.Currency.Add(currency);
                try
                {
                    await unitOfWork.CommitAsync();
                }
                catch
                {
                    return;
                }
            }
        }


        private Store.Entity.Currency GetLastFromDB()
        {
            return unitOfWork.Currency.GetLast();
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
