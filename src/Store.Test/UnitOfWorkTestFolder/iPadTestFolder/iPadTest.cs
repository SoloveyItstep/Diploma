using Microsoft.Data.Entity;
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

namespace Store.Test.UnitOfWorkTestFolder.iPadTestFolder
{
    public class iPadTest
    {
        IUnitOfWork unOfWork;
        IUnitOfWork unitOfWork { get {
                if(unOfWork == null)
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
                    new CurrencyRepository(context));
                }
                return unOfWork;
            } }

        [Fact]
        public async void FirstTest()
        {
            Categories category = await unitOfWork.Categories
                .Find(cat => cat.CategoryName == "iPad");
            Assert.NotNull(category);

            var apple = await unitOfWork.Apple.Find(a => a.AppleID == 7);
            Assert.NotNull(apple);

            var name = await unitOfWork.DetailNames.Find(d => d.DetailNamesID == 1);

            var details = new ProductDetails()
            {
                DetailNames = name,
                Measure = "inch",
                Value = "241.2x185.7x9.4",
                Other = "",
                Apple = apple,
                AppleID = apple.AppleID
            };

            unitOfWork.ProductDetails.Add(details);
            Int32 result = unitOfWork.Commit();
            Assert.Equal(result, 1);

        }

        //var apple; = new Apple()
        //{
        //    Categories = category,
        //    Construction = "",
        //    Model = "",
        //    Name = "iPad",
        //    Price = 396,
        //    Subcategory = "4",
        //    Type = "Tablet",
        //    Url = ""
        //};

        //unitOfWork.Apple.Add(apple);
        //Int32 result = unitOfWork.Commit();
        //Assert.Equal(1, result);

        //===================================
        //var color = new Color
        //{
        //    ColorName = "Black"
        //};

        //unitOfWork.Color.Add(color);
        //Int32 result = unitOfWork.Commit();
        //Assert.Equal(result, 1);
        //===========================================
        //var name = await unitOfWork.DetailNames.Find(d => d.DetailNamesID == 38);

        //var details = new ProductDetails()
        //{
        //    DetailNames = name,
        //    Measure = "",
        //    Value = "241.2x185.7x9.4",
        //    Other = "",
        //    Apple = apple,
        //    AppleID = apple.AppleID
        //};

        //unitOfWork.ProductDetails.Add(details);
        //Int32 result = unitOfWork.Commit();
        //Assert.Equal(result, 1);

        //====================================
        //var color = await unitOfWork.Color.Find(c => c.ColorID == 1);

        //unitOfWork.AppleColor.Add(new AppleColor()
        //{
        //    Apple = apple,
        //    AppleID = apple.AppleID,
        //    ColorID = color.ColorID,
        //    Color = color,
        //    Count = 12
        //});

        //Int32 r = unitOfWork.Commit();
        //Assert.Equal(r, 1);
    }
}
