using Microsoft.AspNet.Mvc;
using AppleStore.Models;
using System.Security.Claims;
using System;
using AppleStore.ViewModels.Account;

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
        //[HttpPost]
        public IActionResult Login()
        {

            return PartialView(new LoginViewModel());
        }
        [HttpPost]
        public IActionResult Register(String id)
        {
            if(id == "EN")
                return PartialView("Register.en-US",new RegisterViewModel());
            return PartialView("Register.ru-RU", new RegisterViewModel());
        }

        
    }
}
