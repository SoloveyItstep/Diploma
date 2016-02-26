using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Repository.UnitOfWorks;
using Microsoft.AspNet.Http;
using AppleStore.ViewModels.Account;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("[controller]/")]
    public class AccessoriesController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        public AccessoriesController(IUnitOfWork unitOfWork)
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
            var mac = await unitOfWork.FindByCategoryNameInclude(a => a.Categories.CategoryName == "Accessories");
            return new ObjectResult(mac);
        }

        [Route("{id}")]
        public async Task<IActionResult> AccessoriesItem(Int32 id)
        {
            HttpContext.Session.SetInt32("currentitem", id);
            var watch = await unitOfWork.Apple.Find(a => a.AppleID == id);
            String language = GetLanguage();
            ViewBag.Name = watch.Name;
            if (language == "EN")
                return View("AccessoriesItem.en-US", new LoginViewModel());
            return View("AccessoriesItem.ru-RU", new LoginViewModel());
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
