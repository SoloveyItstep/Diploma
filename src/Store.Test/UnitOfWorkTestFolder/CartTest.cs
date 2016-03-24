using Store.Context.Context;
using Store.Entity;
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
    public class CartTest
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
        public async void GetDataByDictionary()
        {
            var dict = new Dictionary<Int32, Int32>();
            dict.Add(142, 1);
            dict.Add(143, 1);
            dict.Add(144, 1);

            var apple = await unitOfWork.GetCartData(dict);
            Assert.NotNull(apple);
            Assert.NotEmpty(apple);
        }

    }
}
