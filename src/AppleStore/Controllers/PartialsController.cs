using Microsoft.AspNet.Mvc;
using AppleStore.Models;
using System.Security.Claims;
using System;
using AppleStore.ViewModels.Account;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Repository.UnitOfWorks;
using Store.Entity;
using AppleStore.DataServices.Cart.Interfaces;
using AppleStore.DataServices.Currency.Interfaces;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Identity;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class PartialsController : Controller
    {
        protected readonly ApplicationDbContext userContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICart cart;
        private readonly ICurrency currency;
        private readonly UserManager<ApplicationUser> userManager;

        public PartialsController(ApplicationDbContext context, IUnitOfWork unitOfWork, ICart cart, ICurrency currency, UserManager<ApplicationUser> userManager)
        {
            this.userContext = context;
            this.unitOfWork = unitOfWork;
            this.cart = cart;
            this.currency = currency;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Login()
        {
            String language = HttpContext.Session.GetString("language");
            String user = User.Identity.Name;
            ViewBag.ReturnUrl = "/Home/Index";
            if (user != null && user != "")
            {
                if (language == "EN" || language == null)
                    return PartialView("LogOut.en-US", user);
                return PartialView("LogOut.ru-RU", user);
            }
            
            if (language == "EN" || language == null)
                return PartialView("Login.en-US", new LoginViewModel());
            return PartialView("Login.ru-RU", new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Register()
        {
            String language = HttpContext.Session.GetString("language");
            if(language == "EN" || language == null)
                return PartialView("Register.en-US",new RegisterViewModel());
            return PartialView("Register.ru-RU", new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Logout()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == "EN" || language == null)
                return PartialView("LogOut.en-US", User.Identity.Name);
            return PartialView("LogOut.ru-RU", User.Identity.Name);
        }


 //TODO: Change Cart Connection
        //[HttpPost]
        public async Task<IActionResult> Cart()
        {
            //====Cart Data==============
            Dictionary<Int32, Int32> cart = HttpContext.Session.
                    GetObjectFromJson<Dictionary<Int32, Int32>>("cart");
            String language = HttpContext.Session.GetString("language");
            if (cart == null)
                cart = new Dictionary<int, int>();
            var apple = await unitOfWork.GetCartData(cart);
            Dictionary<Int32,Decimal> price = new Dictionary<Int32, Decimal>();
            foreach (var a in apple)
                price.Add(a.AppleID, a.Price);

            HttpContext.Session.SetObjectAsJson("price", price);
            //=========Return View================
            
            if (language == "EN" || language == null)
                return View("Cart.en-US",apple);
            return View("Cart.ru-RU",apple);
        }
        
        [HttpPost]
        public async Task<IActionResult> Ordering()
        {
            String language = HttpContext.Session.GetString("language");
            var count = HttpContext.Session.GetObjectFromJson<Dictionary<Int32, Int32>>("cart");
            var apple = await cart.GetCartDataInDictionary(count);
            var ua = await currency.GetCurrency();

            var login = new LoginNewUserOrderViewModel(language);
            login.Apple = apple;
            login.Currency = ua;

            var user = userManager.Users.Where(u => u.Id == User.GetUserId()).FirstOrDefault();
            if(user != null)
            {
                login.UserName = user.UserName;
                login.Address = user.Address;
                login.City = user.City;
                login.Email = user.Email;
                login.Phone = user.PhoneNumber;
            }

            if (language == "EN" || language == null)
            {
                return PartialView("PlaceOrder-US", login);
            }
            
            return PartialView("PlaceOrder-RU", login);
        }

        [HttpPost]
        [Route("steadycustomerordering")]
        public IActionResult SteadyCustomer()
        {
            String language = HttpContext.Session.GetString("language");
            var user = userManager.Users.Where(u => u.Id == User.GetUserId()).FirstOrDefault();

            var login = new LoginNewUserOrderViewModel(language);
            login.Address = user.Address;
            login.City = user.City;
            login.Email = user.Email;
            login.Language = language;
            login.Phone = user.PhoneNumber;

            if(language == "EN")
            {
                return PartialView("SteadyCustomerOrderingEn", login);
            }
            return PartialView("SteadyCustomerOrderingRu", login);
        }

        public IActionResult PlacedOrderInfo(Boolean id)
        {
            String language = HttpContext.Session.GetString("language");
            ViewData["Placed"] = id;
            return View("PlacedOrderInfo", language);
        }
    }
}
