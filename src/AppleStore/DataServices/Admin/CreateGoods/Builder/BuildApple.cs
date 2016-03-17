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
        public BuildApple(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Apple apple { get; set; }
        public async Task CreateDetails()
        {
            foreach(var item in apple.ProductDetails)
            {
                var detail = await CheckDetail(item);
                if(detail != null)
                {
                    apple.ProductDetails.Add(detail);
                }
            }
            await unitOfWork.CommitAsync();
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
            detail.DetailNames = detailName;
            return detail;
        }

        public void CreateImages(IList<String> pathList)
        {
            foreach(var path in pathList)
            {
                var image = new Image()
                {
                    ColorID = 1,
                    Size = "large",
                    Apple = apple,
                    AppleID = apple.AppleID,
                    Path = ".."+path,
                };
                unitOfWork.Image.Add(image);
            }
            unitOfWork.Commit();
        }

        public void CreateNewApple()
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
            unitOfWork.Commit();
            apple.AppleID = unitOfWork.Apple.Find(a => a.Categories == apple.Categories &&
                            a.Construction == apple.Construction && a.Model == apple.Model &&
                            a.Name == apple.Name && a.Price == apple.Price &&
                            a.Subcategory == apple.Subcategory && a.Type == apple.Type &&
                            a.Url == apple.Url).Result.AppleID;
        }

        public string GetUrl()
        {
            return "/"+apple.Categories.CategoryName+"/"+apple.AppleID;
        }
    }
}
