using Microsoft.Data.Entity;
using Store.Repository.Repositories.Interfaces;
using Store.Entity;
using Store.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class ProductDetailsRepository: Repository<ProductDetails>, IProductDetailsRepository<ProductDetails>
    {
        protected readonly new IStoreContext context;
        public ProductDetailsRepository(IStoreContext context)
            : base(context)
        { }

        public async Task<ProductDetails[]> GetByAppleIDInclude(Int32 id)
        {
            return await (context as DbContext)
                .Set<ProductDetails>().Where(p => p.AppleID == id)
                .Include(p => p.DetailNames).ToArrayAsync();
        }
    }
}
