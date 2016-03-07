using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Entity;
using Microsoft.Data.Entity;
using Store.Repository.UnitOfWorks;
using Currency;
using Currency.Entity;
using Microsoft.AspNet.Http;
using AppleStore.Models;
using AppleStore.DataServices.Currency.Interfaces;
using AppleStore.DataServices.Cart;
using AppleStore.DataServices.Cart.Interfaces;
using Microsoft.AspNet.Mvc.Filters;
using AppleStore.Services;

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
            return await currency.GetCurrency();
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
        public async Task<Dictionary<Int32,Decimal>> GetPrice()
        {
            cart.GetHttpContext(HttpContext);
            return await cart.GetPrice();
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

//TODO: send cart data (need user)
        [Route("placeorder")]
        [HttpPost]
        public async Task<Boolean> PlaceAnOrder()
        {
            cart.GetHttpContext(HttpContext);
            var Cart = cart.GetCounts();
            var Price = await cart.GetPrice();
            var apple = await cart.GetData();
            var user = new ApplicationUser();
            await emailSender.SendOrder(apple, Cart, Price, user);

            return true;
        }


    }
}
