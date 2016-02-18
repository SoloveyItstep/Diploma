using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IColorRepository<T>: IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Int32 appleID);
    }
}
