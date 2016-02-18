using Microsoft.Data.Entity;
using Store.Repository.Repositories.Interfaces;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Entity.Context;

namespace Store.Repository.Repositories
{
    public class DetailNamesRepository: Repository<DetailNames>, IDetailNamesRepository<DetailNames>
    {
        protected readonly new IStoreContext context;
        public DetailNamesRepository(IStoreContext context)
            : base(context)
        { }

        public async Task<DetailNames[]> GetWidthAllProductDetails(string detailName)
        {
            return await (context as DbContext).Set<DetailNames>()
                .Where(d => d.Name == detailName).Include(d => d.ProductDetails)
                .ToArrayAsync();
        }

        public async Task<DetailNames[]> GetWidthAllProductDetails(int detailManesID)
        {
            return await (context as DbContext).Set<DetailNames>()
                .Where(d => d.DetailNamesID == detailManesID).Include(d => d.ProductDetails)
                .ToArrayAsync(); ;
        }
    }
}
