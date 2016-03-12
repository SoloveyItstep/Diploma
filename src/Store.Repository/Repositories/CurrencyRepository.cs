using Store.Entity;
using Store.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Store.Entity.Context;
using Microsoft.Data.Entity;

namespace Store.Repository.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository<Currency>
    {
        protected readonly new IStoreContext context;
        public CurrencyRepository(IStoreContext context)
            : base(context)
        { this.context = context; }
        
        public async Task<Boolean> DateExist(DateTime date)
        {
            Currency currency = await (context as DbContext).Set<Currency>()
                .Where(c => c.Date == date.ToShortDateString()).FirstOrDefaultAsync();
            if (currency == null)
                return false;
            return true;
        }
        //public async Task<Currency> GetLastAsync()
        //{
        //    return await context.Currency.LastAsync();
        //}
        public Decimal GetLastCurrency()
        {
            var currency = (context as DbContext).Set<Currency>().Last();
            return Decimal.Parse(currency.CurrencyUSD);
        }

        public Currency GetLast()
        {
            return (context as DbContext).Set<Currency>().Last();
        }
    }
}
