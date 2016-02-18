using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Repositories.Interfaces
{
    public interface IDetailNamesRepository<T>: IRepository<T> where T : class
    {
        Task<T[]> GetWidthAllProductDetails(Int32 detailManesID);
        Task<T[]> GetWidthAllProductDetails(String detailName);
    }
}
