using Store.Entity.Context;
using Store.Entity.Order;
using Store.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class AppleOrdersRepository: Repository<AppleOrders>, IAppleOrdersRepository<AppleOrders>
    {
        protected readonly new IStoreContext context;
        public AppleOrdersRepository(IStoreContext context)
            : base(context)
        { }
    }
}
