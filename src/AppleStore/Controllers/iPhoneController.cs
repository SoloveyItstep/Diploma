using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Repository.UnitOfWorks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("[controller]/")]
    public class iPhoneController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        public iPhoneController(IUnitOfWork unitOfWork)
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
            var mac = await unitOfWork.FindByCategoryNameInclude(a => a.Categories.CategoryName == "iPhone");
            return new ObjectResult(mac);
        }

        [Route("element/{id}")]
        public IActionResult iPhoneItem(Int32 id)
        {
            return View(id);
        }
    }
}
