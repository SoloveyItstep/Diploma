using Microsoft.Data.Entity;
using System.Threading.Tasks;
using Store.Entity;
using Store.Entity.Context;
using Store.Entity.Order;

namespace AppleStore.Context.Context
{
    public class StoreContext : DbContext, IStoreContext
    {
        public DbSet<Apple> Apple { get; set; }
        public DbSet<AppleColor> AppleColor { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<DetailNames> DetailNames { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Store.Entity.Currency> Currency { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<AppleOrders> AppleOrders { get; set; }
        public int Save()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Data Source=MYPC;Initial Catalog=AppleStore;Integrated Security=True");
        }
    }
}
