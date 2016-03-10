using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IOrdersRepository<T>: IRepository<T> where T : class
    {
        Task<T[]> GetNotExecuted();
        Task<T> GetByOrderId(String orderID);
        String GetLastOrderNumber();
        Task<T[]> GetAllOrdersInclude();
    }
}
