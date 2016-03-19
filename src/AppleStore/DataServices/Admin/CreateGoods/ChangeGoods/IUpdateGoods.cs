using Store.Entity;
using System;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Admin.CreateGoods.ChangeGoods
{
    public interface IUpdateGoods
    {
        Task<Apple> Update(Apple apple);
    }
}
