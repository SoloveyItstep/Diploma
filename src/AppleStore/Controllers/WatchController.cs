using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Repository.UnitOfWorks;
using Store.Entity;
using Microsoft.AspNet.Http;
using AppleStore.Models;
using AppleStore.ViewModels.Account;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("[controller]/")]
    public class WatchController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        public WatchController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: /<controller>/
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var mac = await unitOfWork.FindByCategoryNameInclude(a => a.Categories.CategoryName == "Watch");
            return new ObjectResult(mac);
        }

        [Route("{id}")]
        public async Task<IActionResult> WatchItem(Int32 id)
        {
            var watch = await unitOfWork.Apple.Find(a => a.AppleID == id);
            if (watch == null)
                return RedirectToAction("Error","Home","No such item was found");
            String language = GetLanguage();
            ViewBag.Name = watch.Name;
            HttpContext.Session.SetInt32("currentitem", id);
            
            if (language == "EN")
                return View("WatchItem.en-US",new ViewModels.Account.LoginViewModel());
            return View("WatchItem.ru-RU", new ViewModels.Account.LoginViewModel());
        }

        private String GetLanguage()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == null)
                language = "EN";
            return language;
        }

    }
}
