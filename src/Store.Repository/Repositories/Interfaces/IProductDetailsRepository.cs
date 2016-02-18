using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IProductDetailsRepository<T>: IRepository<T> where T : class
    {
        Task<T[]> GetByAppleIDInclude(Int32 id);
    }
}
