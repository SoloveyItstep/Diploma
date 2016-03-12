using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface ICurrencyRepository<T>: IRepository<T> where T:class
    {
        Task<Boolean> DateExist(DateTime date);
        //Task<T> GetLastAsync();
        Decimal GetLastCurrency();
        T GetLast();
    }
}
