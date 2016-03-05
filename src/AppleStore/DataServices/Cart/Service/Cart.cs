using AppleStore.DataServices.Cart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Store.Entity;
using Store.Repository.UnitOfWorks;

namespace AppleStore.DataServices.Cart.Service
{
    public class Cart : ICart
    {
        private HttpContext context;
        private IUnitOfWork unitOfWork;
        private ICartData cartData;
        public Cart(ICartData cartData, IUnitOfWork unitOfWork)
        {
            this.cartData = cartData;
            this.unitOfWork = unitOfWork;
        }

        public Dictionary<int, int> GetCounts()
        {
            return cartData.GetCount();
        }

        public async Task<Apple[]> GetData()
        {
            var cart = cartData.GetCount();
            var apple = await unitOfWork.GetCartData(cart);

            var price = new Dictionary<Int32, Decimal>();
            foreach (var a in apple)
                price.Add(a.AppleID, a.Price);

            cartData.AddPrice(price);

            return apple;
        }

        public void GetHttpContext(HttpContext context)
        {
            this.context = context;
            cartData.GetContext(context);
        }

        public async Task<Dictionary<int, decimal>> GetPrice()
        {
            return await cartData.GetPrice();
        }

        public bool RemoveItem(int id)
        {
            var count = cartData.GetCount();
            count.Remove(id);
            cartData.AddCount(count);
            return true;
        }

        public bool UpdateItemCount(int id, int count)
        {
            var cart = cartData.GetCount();
            if (count == 0)
                cart.Remove(id);
            else if (cart.ContainsKey(id))
                cart[id] = count;
            else
                cart.Add(id, count);
            return true;

        }

        public bool AddCount(int id, int count)
        {
            var cart = cartData.GetCount();
            if (cart.ContainsKey(id))
                cart[id] += count;
            else
                cart.Add(id, count);
            return true;
        }

        public async Task<Dictionary<Apple, Int32>> GetCartDataInDictionary(Dictionary<Int32, Int32> count)
        {
            return await cartData.GetCartData(count);
        }
    }
}
