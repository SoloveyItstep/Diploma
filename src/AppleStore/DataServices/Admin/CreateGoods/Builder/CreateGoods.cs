using AppleStore.DataServices.Admin.CreateGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.Net.Http.Server;
using Microsoft.AspNet.Http;

namespace AppleStore.DataServices.Admin.CreateGoods.Builder
{
    public class CreateGoods : ICreateGoods
    {
        private readonly IBuildApple buildApple;
        private readonly IHostingEnvironment hostingEnv;

        public CreateGoods(IBuildApple buildApple, IHostingEnvironment env)
        {
            this.buildApple = buildApple;
            this.hostingEnv = env;
        }

        public IList<String> SaveImages(IFormFileCollection files, IList<String> lst, String category)
        {
            lst = lst == null ? new List<String>() : lst;
            long size = 0;
            Int32 count = 0;
            String date = DateTime.Now.ToString("ddMMyyyyHHmmss");
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var arr = filename.Split('.');
                String name = "";
                for (int i = 0; i < arr.Length - 1; ++i)
                    name += arr[i];

                name += (date + count.ToString() +"."+ arr[arr.Length - 1]);
                
                ++count;
                filename = hostingEnv.WebRootPath + $@"\images\Data\{category}\{name}";
                size += file.Length;
                file.SaveAs(filename);
                lst.Add(filename);
            }
            
            return lst;
        }

        async Task<string> ICreateGoods.CreateGoods(Apple apple, IList<String> imagesPathList)
        {
            buildApple.apple = apple;
            buildApple.CreateNewApple();
            buildApple.CreateImages(imagesPathList);
            await buildApple.CreateDetails();
            return buildApple.GetUrl();
        }
    }
}
