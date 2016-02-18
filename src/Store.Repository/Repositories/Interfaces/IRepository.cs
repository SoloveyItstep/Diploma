using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T[]> GetAllInclude(params Expression<Func<T,Object>>[] include);
        Task<T> GetFirst();
        Task<T> Find(Expression<Func<T, Boolean>> predicate);
        Task<T[]> FindAllInclude(Expression<Func<T, Boolean>> predicate);

        void Add(T entity);
        void Remove(T entity);
        Int32 Save();
        Task<Int32> SaveAsync();
    }
}
