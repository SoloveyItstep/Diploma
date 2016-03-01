using AppleStore.DataServices.Cart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using AppleStore.Models;
using AppleStore.DataServices.Currency.Interfaces;

namespace AppleStore.DataServices.Cart.Service
{
    public class CartData : ICartData
    {
        private HttpContext context;
        private ICurrency currency;
        public CartData(ICurrency currency)
        {
            this.currency = currency;
        }
        public void AddCount(Dictionary<int, int> data)
        {
            context.Session.SetObjectAsJson("cart", data);
        }

        public void AddPrice(Dictionary<int, decimal> data)
        {
            context.Session.SetObjectAsJson("price", data);
        }

        public void GetContext(HttpContext context)
        {
            this.context = context;
        }

        public Dictionary<int, int> GetCount()
        {
            var cart = context.Session.GetObjectFromJson<Dictionary<Int32, Int32>>("cart");
            if (cart == null)
                cart = new Dictionary<int, int>();
            return cart;
        }

        public async Task<Dictionary<int, decimal>> GetPrice()
        {
            var price = context.Session.GetObjectFromJson<Dictionary<Int32, Decimal>>("price");
            if (price == null)
                price = new Dictionary<int, Decimal>();
            if (context.Session.GetString("language") == "EN")
                return price;

            var rusPrice = new Dictionary<Int32, Decimal>();
            var cur = await currency.GetCurrency();
            foreach (var key in price.Keys)
                rusPrice.Add(key, price[key] * cur);

            return rusPrice;
        }
    }
}
