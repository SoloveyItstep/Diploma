using Microsoft.AspNet.Http;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Cart.Interfaces
{
    public interface ICartData
    {
        void GetContext(HttpContext context);
        Task<Dictionary<Int32,Decimal>> GetPrice();
        Dictionary<Int32, Int32> GetCount();
        void AddPrice(Dictionary<Int32, Decimal> data);
        void AddCount(Dictionary<Int32, Int32> data);
        Task<Dictionary<Apple, Int32>> GetCartData(Dictionary<Int32,Int32> count);
    }
}
