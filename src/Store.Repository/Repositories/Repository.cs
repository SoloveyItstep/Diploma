using Store.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using Store.Entity.Context;

namespace Store.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IStoreContext context;
        public Repository(IStoreContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            (context as DbContext).Set<T>().Add(entity);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            var obj = await (context as DbContext).Set<T>().Where(predicate).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<T[]> FindAllInclude(Expression<Func<T, bool>> predicate)
        {
            var obj = (context as DbContext).Set<T>().AsQueryable();
            var v = await obj.Where(predicate).ToArrayAsync();
            return v;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await (context as DbContext).Set<T>().ToListAsync();
        }

        public async Task<T[]> GetAllInclude(params Expression<Func<T,Object>>[] include)
        {
            var obj = (context as DbContext).Set<T>().AsQueryable();

            foreach(var item in include)
            {
                obj = obj.Include(item);
            }
            var t = await obj.ToArrayAsync();
            return t;
        }

        public async Task<T> GetFirst()
        {
            return await (context as DbContext).Set<T>().FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            (context as DbContext).Remove(entity);
        }

        public Int32 Save()
        {
            return (context as DbContext).SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await (context as DbContext).SaveChangesAsync();
        }
    }
}
