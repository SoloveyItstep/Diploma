using AppleStore.DataServices.Admin.CreateGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Entity;
using Store.Repository.UnitOfWorks;

namespace AppleStore.DataServices.Admin.CreateGoods.Builder
{
    public class BuildApple : IBuildApple
    {
        private readonly IUnitOfWork unitOfWork;
        private Int32 Id { get; set; }
        private ICollection<ProductDetails> detailsList { get; set; }

        public BuildApple(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Apple apple { get; set; }
        public async Task CreateDetails()
        {
            detailsList = apple.ProductDetails;
            apple.ProductDetails = new List<ProductDetails>();
            foreach (var item in detailsList)
            {
                var detail = await CheckDetail(item);
                if(detail != null)
                {
                    var obj = new ProductDetails()
                    {
                        Apple = apple,
                        AppleID = Id,
                        Measure = "",
                        Other = "",
                        DetailNames = detail.DetailNames,
                        Value = detail.Value
                    };
                    apple.ProductDetails.Add(obj);
                    await unitOfWork.CommitAsync();
                }
            }

        }

        private async Task<ProductDetails> CheckDetail(ProductDetails detail)
        {
            if (detail.Value == "" || detail.Value == null)
                return null;
            String Name = detail.DetailNames.Name;
            var detailName = await unitOfWork.DetailNames.Find(d => d.Name == Name);
            if (detailName == null)
            {
                detailName = new DetailNames()
                {
                    Name = Name,
                };
                unitOfWork.DetailNames.Add(detailName);
                await unitOfWork.CommitAsync();
            }
            detail.Measure = "";
            detail.Other = "";
            detail.DetailNames = detailName;
            return detail;
        }

        public void CreateImages(IList<String> pathList)
        {
            if (pathList != null)
            {
                foreach (var path in pathList)
                {
                    var image = new Image()
                    {
                        ColorID = 1,
                        Size = "large",
                        Apple = apple,
                        AppleID = Id,
                        Path = ".." + path,
                    };
                    unitOfWork.Image.Add(image);
                }
                unitOfWork.Commit();
            }
        }

        public async Task CreateNewApple()
        {
            var obj = new Apple()
            {
                Categories = apple.Categories,
                Construction = apple.Construction,
                Model = apple.Model,
                Name = apple.Name,
                Subcategory = apple.Subcategory,
                Type = apple.Type,
                Url = apple.Url,
                Price = apple.Price
            };
            unitOfWork.Apple.Add(obj);
            Int32 commit = unitOfWork.Commit();
            Apple item = await unitOfWork.Apple.Find(a => a.Construction == apple.Construction && a.Model == apple.Model &&
                            a.Name == apple.Name && a.Subcategory == apple.Subcategory && a.Type == apple.Type &&
                            a.Url == apple.Url);
                Id = item.AppleID;
            detailsList = apple.ProductDetails;
            Categories categ = apple.Categories;
            apple = item;
            apple.ProductDetails = detailsList;
            apple.Categories = categ;
        }

        public string GetUrl()
        {
            return "/"+apple.Categories.CategoryName+"/"+Id;
        }
    }
}
