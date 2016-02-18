using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IImageRepository<T>: IRepository<T> where T : class
    {
        Task<T[]> GetBySizeName(Int32 appleID,String size);
    }
}
