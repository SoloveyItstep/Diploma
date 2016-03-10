using Store.Entity.Order;
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
    public class OrdersRepository : Repository<Orders>, IOrdersRepository<Orders>
    {
        protected readonly new IStoreContext context;
        public OrdersRepository(IStoreContext context)
            :base(context)
        {
            this.context = context;
        }

        public async Task<Orders> GetByOrderId(string orderID)
        {
            return await (context as DbContext).Set<Orders>().Where(o => o.OrderNumber == orderID)
                .Include(o => o.AppleOrders).ThenInclude(a => a.Apple)
                .FirstOrDefaultAsync();
        }

        public async Task<Orders[]> GetNotExecuted()
        {
            return await (context as DbContext).Set<Orders>().Where(o => o.Status != "executed")
                .Include(o => o.AppleOrders).ThenInclude(a => a.Apple).ToArrayAsync();
        }

        public String GetLastOrderNumber()
        {
            if(context.Orders.Any())
                return (context as DbContext).Set<Orders>().Last().OrderNumber;
            return null;
        }

        public async Task<Orders[]> GetAllOrdersInclude()
        {
            return await (context as DbContext).Set<Orders>().Include(ord => ord.AppleOrders)
                .ThenInclude(a => a.Apple).ToArrayAsync();
        }
    }
}
