using Store.Repository.Repositories.Interfaces;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using Store.Entity.Context;

namespace Store.Repository.Repositories
{
    public class AppleRepository : Repository<Apple>, IAppleRepository<Apple>
    {
        protected readonly new IStoreContext context;
        public AppleRepository(IStoreContext context)
            :base(context)
        {
            this.context = context;
        }
        public async Task<Apple> GetFirstInclude(params Expression<Func<Apple, object>>[] include)
        {
            return await (context as DbContext).Set<Apple>()
                .Include(a => a.Categories)
                .Include(a => a.AppleColor).ThenInclude(ac => ac.Color)
                .Include(a => a.ProductDetails).ThenInclude(a => a.DetailNames)
                .Include(a => a.AppleImage).FirstOrDefaultAsync();
        }

        public async Task<Apple> GetOneInclude(Int32 id)
        {
            var apple = await (context as DbContext).Set<Apple>().Where(a => a.AppleID == id)
                .Include(a => a.ProductDetails).ThenInclude(a => a.DetailNames)
                .Include(a => a.AppleColor).ThenInclude(a => a.Color)
                .Include(a => a.AppleImage).FirstOrDefaultAsync();
            return apple;
        }
    }
}
