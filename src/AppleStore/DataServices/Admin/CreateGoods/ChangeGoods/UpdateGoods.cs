using System;
using System.Linq;
using System.Threading.Tasks;
using Store.Entity;
using Store.Repository.UnitOfWorks;

namespace AppleStore.DataServices.Admin.CreateGoods.ChangeGoods
{
    public class UpdateGoods: IUpdateGoods
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateGoods(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Apple> Update(Apple apple)
        {
            var item = await unitOfWork.Apple.GetOneInclude(apple.AppleID);
            item.Construction = apple.Construction;
            item.Model = apple.Model;
            item.Name = apple.Name;
            item.Price = apple.Price;
            item.Subcategory = apple.Subcategory;
            item.Type = apple.Type;
            item.Url = apple.Url;

            await unitOfWork.Apple.SaveAsync();
            Int32 i = 0;
            foreach (var v in apple.ProductDetails)
            {
                item.ProductDetails.ToArray()[i].Value = v.Value;
                ++i;
            }
            await unitOfWork.Apple.SaveAsync();
            return await unitOfWork.Apple.GetOneInclude(item.AppleID);
        }
    }
}
