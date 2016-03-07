using Microsoft.Data.Entity;
using Store.Entity.Order;
using System;
using System.Threading.Tasks;

namespace Store.Entity.Context
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
        DbSet<Currency> Currency { get; set; }
        DbSet<Orders> Orders { get; set; }
        Int32 Save();
        Task<Int32> SaveAsync();
    }
}
