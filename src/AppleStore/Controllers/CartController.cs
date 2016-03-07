using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using AppleStore.ViewModels.Account;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class CartController : Controller
    {
        
        public IActionResult Index()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == "EN" || language == null)
                return View("Cart.en-US");
            return View("Cart.ru-RU");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceAnOrder(LoginNewUserOrderViewModel orderModel)
        {
            if (ModelState.IsValid)
            {
                String user = orderModel.UserName;

                if(orderModel.Payment == "PayPal")
                {
                    user = "";
                }
            }

            return View();
        }
    }
}
