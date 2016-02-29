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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class PartialsController : Controller
    {
        protected readonly ApplicationDbContext userContext;
        private readonly IUnitOfWork unitOfWork;

        public PartialsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            this.userContext = context;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Login()
        {
            String language = HttpContext.Session.GetString("language");
            String user = User.Identity.Name;
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

        //[HttpPost]
        public async Task<IActionResult> Cart()
        {
            //====Cart Data==============
            Dictionary<Int32, Int32> cart = HttpContext.Session.
                    GetObjectFromJson<Dictionary<Int32, Int32>>("cart");
            if (cart == null)
            {
                cart = new Dictionary<int, int>();
                cart.Add(40, 1);
                cart.Add(41, 2);
                cart.Add(43, 3);
                cart.Add(6, 1);
                cart.Add(7, 5);
                cart.Add(8, 2);
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }

            var apple = await unitOfWork.GetCartData(cart);
            Dictionary<Int32,Decimal> price = new Dictionary<Int32, Decimal>();
            foreach (var a in apple)
                price.Add(a.AppleID, a.Price);

            HttpContext.Session.SetObjectAsJson("price", price);
            //=========Return View================
            String language = HttpContext.Session.GetString("language");
            if (language == "EN" || language == null)
                return View("Cart.en-US",apple);
            return View("Cart.ru-RU",apple);
        }
        
    }
}
