using Microsoft.AspNet.Http;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Admin.CreateGoods.Interfaces
{
    public interface ICreateGoods
    {
        Task<String> CreateGoods(Apple apple, IList<String> imagesPathList);
        IList<String> SaveImages(IFormFileCollection files, IList<String> lst, String category);
    }
}
