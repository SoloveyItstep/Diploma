using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Entity;
using Store.Repository.UnitOfWorks;
using Microsoft.AspNet.Http;
using AppleStore.Models;
using AppleStore.DataServices.Currency.Interfaces;
using AppleStore.DataServices.Cart.Interfaces;
using AppleStore.Services;
using Store.Entity.Order;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("api/[controller]")]
    public class AppleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrency currency;
        private readonly ICart cart;
        private readonly IEmailSender emailSender;

        public AppleController(IUnitOfWork unitOfWork, ICurrency currency, ICart cart, IEmailSender emailSender)
        {
            this.unitOfWork = unitOfWork;
            this.currency = currency;
            this.cart = cart;
            this.emailSender = emailSender;
        }

        [Route("goods")]
        public async Task<IActionResult> GetGoods()
        {
            var apple = await unitOfWork.Apple.GetAll();
            return new ObjectResult(apple);
        }

        [Route("categories")]
        public async Task<IActionResult> SearchData()
        {
            var data = await unitOfWork.GetAppleForSearchIncludeCategories();
            return new ObjectResult(data);
        }

        [Route("categorieslist")]
        public async Task<IActionResult> GetCategoriesList()
        {
            var categories = await unitOfWork.Categories.GetAll();
            return new ObjectResult(categories);
        }

        [Route("category/{id}")]
        public async Task<IActionResult> GetAllCategory(String id)
        {
            var apple = await unitOfWork.GetAllByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("sexteen/{id}")]
        public async Task<IActionResult> GetFirstTwenty(String id)
        {
            var apple = await unitOfWork.GetTwentyByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("aftersexteen/{id}")]
        public async Task<IActionResult> GetLastAfterSexteen(String id)
        {
            var apple = await unitOfWork.GetAllSkypSexteenByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("")]
        public IActionResult Index()
        {
            var apple = unitOfWork.Apple.GetAllInclude();
            return new ObjectResult(apple);
        }

        [Route("main")]
        public IActionResult Main()
        {
            var data = unitOfWork.GetByOneFromCategories();
            return new ObjectResult(data);
        }

        [Route("currency")]
        [HttpPost]
        public async Task<Decimal> Currency()
        {
            var cur = await unitOfWork.Currency.Find(c => c.CurrencyID == 16);
            Decimal uah = Decimal.Parse(cur.CurrencyUSD);
            return uah;
            //var cur = await currency.GetCurrency();
            //if (cur == -1)
            //    return unitOfWork.Currency.GetLastCurrency();
            //return cur;
        }

        [Route("element/{id}")]
        public async Task<IActionResult> GetApple(Int32 id)
        {
            var item = await unitOfWork.Apple.GetOneInclude(id);
            return new ObjectResult(item);
        }

        [Route("getitemid")]
        public Int32 GetCurrentItemID()
        {
            Int32? id = HttpContext.Session.GetInt32("currentitem");
            return id == null? -1: Int32.Parse(id.ToString());
        }

        [Route("cart/{id}")]
        [HttpPost]
        public Boolean AddToCart(Int32 id, Int32 count = 1)
        {
            cart.GetHttpContext(HttpContext);
            return cart.AddCount(id, count);
        }

        //[Route("updatecart/{id}")]
        //[HttpPost]
        //public Boolean UpdateCart(Dictionary<Int32,Int32> id)
        //{
        //    if (id != null)
        //    {
        //        HttpContext.Session.SetObjectAsJson("cart", id);
        //        return true;
        //    }
        //    return false;
        //}

        [Route("getcartdata")]
        [HttpPost]
        public async Task<Apple[]> GetCartData()
        {
            cart.GetHttpContext(HttpContext);
            return await cart.GetData();
        }
        
        [Route("getcartscount")]
        [HttpPost]
        public Dictionary<Int32,Int32> GetCartsCount()
        {
            cart.GetHttpContext(HttpContext);
            return cart.GetCounts();
        }

        [Route("price")]
        [HttpPost]
        public Dictionary<Int32,Decimal> GetPrice()
        {
            cart.GetHttpContext(HttpContext);
            return cart.GetPrice();
        }

        [Route("cartitemremove/{id}")]
        [HttpPost]
        public Boolean RemoveFromCart(Int32 id)
        {
            cart.GetHttpContext(HttpContext);
            return cart.RemoveItem(id);
        }

        [Route("updatecartitem/{id}/{count}")]
        [HttpPost]
        public Boolean UpdateCartItem(Int32 id, Int32 count)
        {
            cart.GetHttpContext(HttpContext);
            return cart.UpdateItemCount(id, count);
        }

        [HttpPost]
        [Route("cardataexist")]
        public Boolean CartDataExist() {
            var crt = HttpContext.Session.GetObjectFromJson<Dictionary<Int32, Int32>>("cart");
            if (crt != null)
                return true;
            return false;
        }

        // send cart data (need user)
        //[Route("placeorder")]
        //[HttpPost]
        //public async Task<Boolean> PlaceAnOrder()
        //{
        //    cart.GetHttpContext(HttpContext);
        //    var Cart = cart.GetCounts();
        //    var Price = await cart.GetPrice();
        //    var apple = await cart.GetData();
        //    var user = new ApplicationUser();
        //    await emailSender.SendOrder(apple, Cart, Price, user);

        //    return true;
        //}

        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("notexecutedorders")]
        [HttpPost]
        public async Task<Orders[]> GetOrders()
        {
            return await unitOfWork.Orders.GetNotExecuted();
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        [Route("orderbynumber/{id}")]
        public async Task<Orders> GetOrderByNumber(String id)
        {
            return await unitOfWork.Orders.GetByOrderId(id);
        }

        [HttpPost]
        [Route("currencyvalue")]
        public String CurrencyValue()
        {
            var currencyValue = HttpContext.Session.GetString("currencyvalue");
            if(currencyValue == null)
            {
                HttpContext.Session.SetString("currencyvalue", "USD");
                currencyValue = "USD";
            }
            return currencyValue;
        }

        [HttpPost]
        [Route("changecurrencyvalue")]
        public void ChangeCurrencyValue()
        {
            var currencyValue = HttpContext.Session.GetString("currencyvalue");
            if (currencyValue == null || currencyValue == "USD")
                HttpContext.Session.SetString("currencyvalue", "UAH");
            else
                HttpContext.Session.SetString("currencyvalue", "USD");
        }

        [HttpPost]
        [Route("searchdata")]
        public Apple[] GetSearchData()
        {
            var search = HttpContext.Session.GetString("search");
            if (search == null)
                return null;
            var apple = unitOfWork.Apple.FindAndIncludeAllTables(a => a.Model.Contains(search));
            return apple;
        }
    }
}
