using Microsoft.AspNet.Mvc;
using AppleStore.Models;
using System.Security.Claims;
using System;
using AppleStore.ViewModels.Account;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class PartialsController : Controller
    {
        protected readonly ApplicationDbContext userContext;

        public PartialsController(ApplicationDbContext context)
        {
            this.userContext = context;
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
    }
}
