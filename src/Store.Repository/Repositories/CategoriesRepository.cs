using Store.Repository.Repositories.Interfaces;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Entity.Context;

namespace Store.Repository.Repositories
{
    public class CategoriesRepository: Repository<Categories>, ICategoriesRepository<Categories>
    {
        protected readonly new IStoreContext context;
        public CategoriesRepository(IStoreContext context)
            :base(context)
        {
            this.context = context;
        }

    }
}
