using Microsoft.Data.Entity;
using Store.Entity;
using Store.Entity.Order;
using System;
using System.Threading.Tasks;

namespace AppleStore.Entity.Context
{
    public interface IStoreContext
    {
        DbSet<Apple> Apple { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<AppleColor> AppleColor { get; set; }
        DbSet<Color> Color { get; set; }
        DbSet<ProductDetails> ProductDetails { get; set; }
        DbSet<DetailNames> DetailNames { get; set; }
        DbSet<Image> Image { get; set; }
        DbSet<Store.Entity.Currency> Currency { get; set; }
        DbSet<Orders> Orders { get; set; }
        DbSet<AppleOrders> AppleOrders { get; set; }
        Int32 Save();
        Task<Int32> SaveAsync();
    }
}
