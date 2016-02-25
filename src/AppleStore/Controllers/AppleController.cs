using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Store.Entity;
using Microsoft.Data.Entity;
using Store.Repository.UnitOfWorks;
using Currency;
using Currency.Entity;
using Microsoft.AspNet.Http;
using AppleStore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("api/[controller]")]
    public class AppleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrencyUSD currency;
        public AppleController(IUnitOfWork unitOfWork, ICurrencyUSD currency)
        {
            this.unitOfWork = unitOfWork;
            this.currency = currency;
        }

        [Route("goods")]
        public async Task<IActionResult> GetGoods()
        {
            var apple = await unitOfWork.Apple.GetAll();
            return new ObjectResult(apple);
        }

        [Route("categories")]
        public async Task<IActionResult> SearchData()
        {
            var data = await unitOfWork.GetAppleForSearchIncludeCategories();
            return new ObjectResult(data);
        }

        [Route("category/{id}")]
        public async Task<IActionResult> GetAllCategory(String id)
        {
            var apple = await unitOfWork.GetAllByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("sexteen/{id}")]
        public async Task<IActionResult> GetFirstTwenty(String id)
        {
            var apple = await unitOfWork.GetTwentyByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("aftersexteen/{id}")]
        public async Task<IActionResult> GetLastAfterSexteen(String id)
        {
            var apple = await unitOfWork.GetAllSkypSexteenByCategoryNameInclude(id);
            return new ObjectResult(apple);
        }

        [Route("")]
        public IActionResult Index()
        {
            var apple = unitOfWork.Apple.GetAllInclude();
            //var apple = unitOfWork.GetByOneFromCategories();
            return new ObjectResult(apple);
        }

        [Route("main")]
        public IActionResult Main()
        {
            var data = unitOfWork.GetByOneFromCategories();
            return new ObjectResult(data);
        }

        [Route("carousel")]
        public Apple[] GetCarouselData()
        {
            return null;
        }

        [Route("currency")]
        [HttpPost]
        public async Task<Decimal> Currency()
        {
            string uah = currency.GetCurrentDateCurrency();
            if (uah == null)
            {
                Data data = currency.GetLastCurrency(DateTime.Now);
                Boolean key = await unitOfWork.Currency.DateExist(data.date);
                if (!key)
                {
                    unitOfWork.Currency.Add(new Store.Entity.Currency()
                    {
                        CurrencyUSD = data.curency.ToString(),
                        Date = data.date.ToShortDateString()
                    });
                    await unitOfWork.CommitAsync();
                    return Decimal.Parse(data.curency);
                }
                uah = data.curency;
            }
            return Decimal.Parse(uah);
            
        }

        [Route("element/{id}")]
        public async Task<IActionResult> GetApple(Int32 id)
        {
            var item = await unitOfWork.Apple.GetOneInclude(id);
            return new ObjectResult(item);
        }

        [Route("getitemid")]
        public Int32 GetCurrentItemID()
        {
            Int32? id = HttpContext.Session.GetInt32("currentitem");
            return id == null? -1: Int32.Parse(id.ToString());
        }

        [Route("cart/{id}")]
        [HttpPost]
        public Boolean AddToCart(Int32 id)
        {
            IList<Int32> cart = HttpContext.Session.GetObjectFromJson<IList<Int32>>("cart");
            if (cart == null)
                cart = new List<Int32>();
            cart.Add(id);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return true;
        }

    }
}
