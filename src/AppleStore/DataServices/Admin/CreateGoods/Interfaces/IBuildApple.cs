using Store.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Admin.CreateGoods.Interfaces
{
    public interface IBuildApple
    {
        Apple apple { get; set; }
        Task CreateNewApple();
        void CreateImages(IList<String> pathList);
        Task CreateDetails();
        String GetUrl();
        
    }
}
