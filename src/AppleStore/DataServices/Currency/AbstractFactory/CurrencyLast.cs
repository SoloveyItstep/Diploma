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

        public void CreateCurrency()
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
            //else if (curr.Date != data.date.ToShortDateString())
            //{
            //    curr.CurrencyUSD = data.curency.ToString();
            //    curr.Date = data.date.ToShortDateString();
            //    try
            //    {
            //        await unitOfWork.CommitAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //}
        }


        private Store.Entity.Currency GetLastFromDB()
        {
  //TODO: changed on async
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
