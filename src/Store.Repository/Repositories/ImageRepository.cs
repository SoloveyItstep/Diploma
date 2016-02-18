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
    public class ImageRepository: Repository<Image>, IImageRepository<Image>
    {
        protected readonly new IStoreContext context;
        public ImageRepository(IStoreContext context)
            : base(context)
        { }

        public async Task<Image[]> GetBySizeName(int appleID, string size)
        {
            return await (context as DbContext).Set<Image>()
                .Where(i => i.AppleID == appleID && i.Size == size).ToArrayAsync();
        }
    }
}
