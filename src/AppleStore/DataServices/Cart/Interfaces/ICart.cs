using Microsoft.AspNet.Http;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Cart.Interfaces
{
    public interface ICart
    {
        void GetHttpContext(HttpContext context);
        Boolean AddCount(Int32 id, Int32 count);
        //Boolean AddPrice(Int32 id);
        Task<Apple[]> GetData();
        Dictionary<Int32, Int32> GetCounts();
        Task<Dictionary<Int32, Decimal>> GetPrice();
        Boolean RemoveItem(Int32 id);

        Boolean UpdateItemCount(Int32 id, Int32 count);
    }
}
