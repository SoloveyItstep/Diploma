using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IAppleRepository<T>: IRepository<T> where T : class
    {
        Task<T> GetFirstInclude(params Expression<Func<T, Object>>[] include);
        Task<T> GetOneInclude(Int32 id);
    }
}
