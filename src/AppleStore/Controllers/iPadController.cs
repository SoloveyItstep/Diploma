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
    [Route("ipad/")]
    public class iPadController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        public iPadController(IUnitOfWork unitOfWork)
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
            var ipad = await unitOfWork.FindByCategoryNameInclude(a => a.Categories.CategoryName == "iPad");
            return new ObjectResult(ipad);
        }

        [Route("{id}")]
        public async Task<IActionResult> iPadItem(Int32 id)
        {
            HttpContext.Session.SetInt32("currentitem", id);
            var watch = await unitOfWork.Apple.Find(a => a.AppleID == id);
            String language = GetLanguage();
            ViewBag.Name = watch.Name;
            if (language == "EN")
                return View("iPadItem.en-US", new LoginViewModel());
            return View("iPadItem.ru-RU", new LoginViewModel());
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
