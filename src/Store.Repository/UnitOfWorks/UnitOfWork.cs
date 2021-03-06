﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Repository.Repositories.Interfaces;
using Store.Entity;
//using Store.Context.Context;
using Store.Entity.Context;
using Microsoft.Data.Entity;
using System.Linq.Expressions;
using Store.Entity.Order;

namespace Store.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IStoreContext context;
        public UnitOfWork(IStoreContext context,
                          IAppleRepository<Apple> appleRepository,
                          ICategoriesRepository<Categories> categoriesRepository,
                          IProductDetailsRepository<ProductDetails> productDetailsRepository,
                          IDetailNamesRepository<DetailNames> detailNamesReporitory,
                          IAppleColorRepository<AppleColor> appleColorRepository,
                          IImageRepository<Image> imageRepository,
                          IColorRepository<Color> colorRepository,
                          ICurrencyRepository<Currency> currency,
                          IOrdersRepository<Orders> orders,
                          IAppleOrdersRepository<AppleOrders> appleOrders)
        {
            this.context = context;
            this.Apple = appleRepository;
            this.Categories = categoriesRepository;
            this.AppleColor = appleColorRepository;
            this.DetailNames = detailNamesReporitory;
            this.Image = imageRepository;
            this.Color = colorRepository;
            this.ProductDetails = productDetailsRepository;
            this.Currency = currency;
            this.Orders = orders;
            this.AppleOrders = appleOrders;
        }

        public IAppleRepository<Apple> Apple { get; set; }
        public IAppleColorRepository<AppleColor> AppleColor { get; set; }
        public ICategoriesRepository<Categories> Categories { get; set; }
        public IColorRepository<Color> Color { get; set; }
        public IDetailNamesRepository<DetailNames> DetailNames { get; set; }
        public IImageRepository<Image> Image { get; set; }
        public IProductDetailsRepository<ProductDetails> ProductDetails { get; set; }
        public ICurrencyRepository<Currency> Currency { get; set; }
        public IOrdersRepository<Orders> Orders { get; set; }
        public IAppleOrdersRepository<AppleOrders> AppleOrders { get; set; }
        public int Commit()
        {
            return Apple.Save();
        }
        public async Task<Int32> CommitAsync()
        {
            return await Apple.SaveAsync();
        }
        public Apple[] GetByOneFromCategories()
        {
            var lst = GetFirstArray().ToArray();
            return lst;
        }
        public async Task<Apple[]> GetAppleForSearchIncludeCategories()
        {

            try {
                return await context.Apple.Include(c => c.Categories).ToArrayAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private IEnumerable<Apple> GetFirstArray()
        {
            var categories = context.Categories.ToList();
            foreach(var cat in categories) {
                yield return (context as DbContext).Set<Apple>()
                    .Where(a => a.Categories.CategoryName == cat.CategoryName)
                    .Include(a => a.AppleImage)
                    .Include(a => a.Categories)
                    .FirstOrDefault();
            }
        }
        public async Task<Apple[]> FindByCategoryNameInclude(Expression<Func<Apple, Boolean>> predicate)
        {
            var apple = await Apple.GetAllInclude();
            return apple.AsQueryable().Where(predicate).ToArray();
        }
        public async Task<Apple[]> GetAllByCategoryNameInclude(String categoryName)
        {
            return await context.Apple.Where(a => a.Categories.CategoryName.ToLower() == categoryName.ToLower())
                     .Include(ac => ac.AppleColor).ThenInclude(a => a.Color)
                    .Include(a => a.ProductDetails).ThenInclude(pd => pd.DetailNames)
                    .Include(a => a.AppleImage)
                    .ToArrayAsync();
        }
        public async Task<Apple[]> GetTwentyByCategoryNameInclude(String categoryName)
        {
            return await context.Apple.Where(a => a.Categories.CategoryName.ToLower() == categoryName.ToLower())
                    .Take(24).Include(ac => ac.AppleColor).ThenInclude(a => a.Color)
                    .Include(a => a.ProductDetails).ThenInclude(pd => pd.DetailNames)
                    .Include(a => a.AppleImage).ToArrayAsync();
        }
        public async Task<Apple[]> GetAllSkypSexteenByCategoryNameInclude(String categoryName)
        {
            var arr = await context.Apple.Where(a => a.Categories.CategoryName.ToLower() == categoryName.ToLower())
                    .Skip(24).Include(ac => ac.AppleColor).ThenInclude(a => a.Color)
                    .Include(a => a.ProductDetails).ThenInclude(pd => pd.DetailNames)
                    .Include(a => a.AppleImage).ToArrayAsync();
            return arr;
        }
        public async Task<Apple[]> GetCartData(Dictionary<int, int> cart)
        {
            var apple = cart.Keys.SelectMany(id => context.Apple.Where(ap => ap.AppleID == id)
                       .Include(ai => ai.AppleImage)
                       .Include(ai => ai.AppleColor)
                       .ThenInclude(im => im.Color)).ToAsyncEnumerable();
            
            return await apple.ToArray();
        }
    }
}
