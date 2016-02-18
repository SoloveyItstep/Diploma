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
    public class AppleColorRepository: Repository<AppleColor>, IAppleColorRepository<AppleColor> 
    {
        protected readonly new IStoreContext context;
        public AppleColorRepository(IStoreContext context)
            : base(context)
        { }

        public async Task<AppleColor> GetByAppleID(int appleID)
        {
            return await (context as DbContext).Set<AppleColor>()
                .Where(x => x.AppleID == appleID)
                .Include(a => a.Color)
                .FirstOrDefaultAsync();
        }

        public async Task<Color> GetListOfColors(int appleID)
        {
            var appleColor = await (context as DbContext).Set<AppleColor>()
                .Include(a => a.Color).FirstOrDefaultAsync();
            return appleColor.Color;
        }
    }
}
