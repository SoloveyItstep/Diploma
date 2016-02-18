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
    public class ColorRepository: Repository<Color>, IColorRepository<Color>
    {
        protected readonly new IStoreContext context;
        public ColorRepository(IStoreContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Color>> GetAll(int appleID)
        {
            var colors = await (context as DbContext).Set<Color>()
                .Include(a => a.AppleColor.Where(ac => ac.AppleID == appleID)).ToListAsync();
            return colors;
        }
    }
}
