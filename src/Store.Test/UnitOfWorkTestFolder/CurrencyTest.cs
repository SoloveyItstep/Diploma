using Store.Context.Context;
using Store.Entity.Context;
using Store.Repository.Repositories;
using Store.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Store.Test.UnitOfWorkTestFolder
{
    public class CurrencyTest
    {
        IUnitOfWork unOfWork;
        IUnitOfWork unitOfWork
        {
            get
            {
                if (unOfWork == null)
                {
                    IStoreContext context = new StoreContext();
                    unOfWork = new UnitOfWork(context,
                    new AppleRepository(context),
                    new CategoriesRepository(context),
                    new ProductDetailsRepository(context),
                    new DetailNamesRepository(context),
                    new AppleColorRepository(context),
                    new ImageRepository(context),
                    new ColorRepository(context),
                    new CurrencyRepository(context),
                    new OrdersRepository(context),
                    new AppleOrdersRepository(context));
                }
                return unOfWork;
            }
        }

        [Fact]
        public async Task GetCurrency()
        {
            var currency = await unitOfWork.Currency.Find(cur => cur.Date == "09.03.2016");
            Assert.NotNull(currency);

            currency = await unitOfWork.Currency.Find(cur => cur.Date == "08.03.2016");
            Assert.Null(currency);
        }

        [Fact]
        public void GetLast()
        {
            var currency = unitOfWork.Currency.GetLast();
            Assert.NotNull(currency);

            var uah = unitOfWork.Currency.GetLastCurrency();
            Assert.NotNull(uah);
            Assert.InRange<Decimal>(uah, 0, 30);
        }
    }
}
