using Store.Repository.Repositories.Interfaces;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Store.Entity.Order;

namespace Store.Repository.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IAppleRepository<Apple> Apple { get; set; }
        ICategoriesRepository<Categories> Categories { get; set; }
        IAppleColorRepository<AppleColor> AppleColor { get; set; }
        IColorRepository<Color> Color { get; set; }
        IProductDetailsRepository<ProductDetails> ProductDetails { get; set; }
        IDetailNamesRepository<DetailNames> DetailNames { get; set; }
        IImageRepository<Image> Image { get; set; }
        ICurrencyRepository<Currency> Currency { get; set; }
        IOrdersRepository<Orders> Orders { get; set; }
        IAppleOrdersRepository<AppleOrders> AppleOrders { get; set; }
        Int32 Commit();
        Task<Int32> CommitAsync();

        Apple[] GetByOneFromCategories();
        Task<Apple[]> GetAppleForSearchIncludeCategories();
        Task<Apple[]> FindByCategoryNameInclude(Expression<Func<Apple, Boolean>> predicate);
        Task<Apple[]> GetAllByCategoryNameInclude(String categoryName);
        Task<Apple[]> GetTwentyByCategoryNameInclude(String categoryName);
        Task<Apple[]> GetAllSkypSexteenByCategoryNameInclude(String categoryName);
        Task<Apple[]> GetCartData(Dictionary<Int32, Int32> cart);
    }
}
