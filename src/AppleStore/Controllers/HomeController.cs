using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Filters;
using AppleStore.ViewModels.Account;

namespace AppleStore.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //public IActionResult Error()
        //{
        //    return View();
        //}

        public IActionResult Ipadlist()
        {
            return PartialView();
        }

        public IActionResult Social()
        {
            return View(new LoginViewModel());
        }

        public IActionResult Error(String id)
        {
            return View("Error", id);
        }

        public IActionResult Search(String id)
        {
            HttpContext.Session.SetString("search", id);
            return View();
        }

        public IActionResult Delivery()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }
    }
}
