using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IAppleColorRepository<T>: IRepository<T> where T : class
    {
        Task<T> GetByAppleID(Int32 appleID);
        Task<Color> GetListOfColors(Int32 appleID);
    }
}
