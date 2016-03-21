using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IAppleRepository<T>: IRepository<T> where T : class
    {
        Task<T> GetFirstInclude(Expression<Func<T, Boolean>> include);
        Task<T> GetOneInclude(Int32 id);
        Task<T[]> GetItemsByListWithId(IList<Int32> list);
        T[] FindAndIncludeAllTables(Expression<Func<T, Boolean>> predicate);
    }
}
