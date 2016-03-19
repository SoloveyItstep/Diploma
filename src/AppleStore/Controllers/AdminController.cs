using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using AppleStore.Models;
using Microsoft.Extensions.CodeGeneration;
using AppleStore.Services;
using AppleStore.Models.RegisterLogin;
using Store.Repository.UnitOfWorks;
using Store.Entity.Order;
using Store.Entity;
using AppleStore.DataServices.Hubs.Interfaces;
using Microsoft.AspNet.Http;
using AppleStore.DataServices.Admin.CreateGoods.Interfaces;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNet.Hosting;
using AppleStore.DataServices.Admin.CreateGoods.ChangeGoods;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly IEmailSender emailSender;
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderHubFacade adminOrder;
        private readonly ICreateGoods createGoods;
        private readonly IHostingEnvironment hostingEnv;
        private readonly IUpdateGoods updateGoods;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IEmailSender emailSender,
                               ApplicationDbContext context,
                               IUnitOfWork unitOfWork,
                               IOrderHubFacade adminOrder,
                               ICreateGoods createGoods,
                               IHostingEnvironment hostingEnv,
                               IUpdateGoods updateGoods,
                               UserManager<ApplicationUser> userManager)
        {
            this.emailSender = emailSender;
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.adminOrder = adminOrder;
            this.createGoods = createGoods;
            this.hostingEnv = hostingEnv;
            this.updateGoods = updateGoods;
            this.userManager = userManager;
        }

        public IActionResult Main()
        {
            ViewData["Role"] = GetRoleName();
            return View();
        }

        public async Task<IActionResult> OrderPage(String id)
        {
            ViewData["Role"] = GetRoleName();
            var order = await unitOfWork.Orders.GetByOrderId(id);
            
            var user = context.Users.Where(u => u.Id == order.UserID).FirstOrDefault();
            ViewData["User"] = user;
            return View(order);
        }

        public async Task<IActionResult> EditOrder(String id)
        {
            ViewData["Role"] = GetRoleName();
            var order = await unitOfWork.Orders.GetByOrderId(id);

            var user = context.Users.Where(u => u.Id == order.UserID).FirstOrDefault();
            ViewData["User"] = user;
            return View(order);
        }

        public IActionResult EditUser(String id, String ReturnUrl = "/Admin/Main")
        {
            ViewData["Role"] = GetRoleName();
            var user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(user);
        }

        public IActionResult ChangeOrder(String orderNumber,String ReturnUrl = "/Admin/Main")
        {
            //var order = await unitOfWork.Orders.GetByOrderId(orderNumber);
            //var lst = new List<Int32>();
            //foreach(var ord in order.AppleOrders)
            //    lst.Add(ord.AppleID);

            //var apple = await unitOfWork.Apple.GetItemsByListWithId(lst);
            ViewData["Role"] = GetRoleName();
            return View("ChangeOrder", orderNumber);
        }

        [HttpPost]
        public async Task<Apple[]> GetOrderItems(String orderNumber)
        {
            var order = await unitOfWork.Orders.GetByOrderId(orderNumber);
            var lst = new List<Apple>();
            foreach (var ord in order.AppleOrders)
                lst.Add(ord.Apple);

            ////var apple = await unitOfWork.Apple.GetItemsByListWithId(lst);

            //var dict = new Dictionary<Apple, Int32>();
            //foreach (var item in order.AppleOrders)
            //{
            //    //var ap = apple.Where(a => a.AppleID == item.AppleID).FirstOrDefault();
            //    dict.Add(item.Apple, item.Count);
            //}
            return lst.ToArray();
        }

        [HttpPost]
        public async Task<Dictionary<Int32,Int32>> GetOrderCount(String orderNumber)
        {
            var order = await unitOfWork.Orders.GetByOrderId(orderNumber);
            var lst = new Dictionary<Int32, Int32>();
            foreach (var ord in order.AppleOrders)
                lst.Add(ord.AppleID,ord.Count);
            return lst;
        }

        [HttpPost]
        public async Task<Boolean> CreateNewOrder(string id, string count, String orderNumber)
        {
            String[] ArrID = id.Split(',');
            String[] ArrCount = count.Split(',');
            var dict = new Dictionary<Int32, Int32>();
            for(Int32 i = 0; i < ArrID.Length; ++i)
            {
                dict.Add(Int32.Parse(ArrID[i]), Int32.Parse(ArrCount[i]));
            }

            String str = orderNumber;
            Orders ord = await unitOfWork.Orders.GetByOrderId(orderNumber);
            ord.Status = "Removed";
            await unitOfWork.CommitAsync();

            String number = unitOfWork.Orders.GetNextOrderNumber();

            Decimal sum = 0;
            IList<Int32> lst = new List<Int32>();
            Dictionary<Int32, Int32> dictCount = new Dictionary<int, int>();
            Dictionary<Int32, Decimal> dictPrice = new Dictionary<int, decimal>();
            foreach(var item in ArrID)
            {
                lst.Add(Int32.Parse(item));
            }

            var apple = await unitOfWork.Apple.GetItemsByListWithId(lst);
            for(Int32 i = 0; i < ArrCount.Length; ++i)
            {
                var price = apple.Where(a => a.AppleID == lst[i]).FirstOrDefault().Price;
                sum += price * Int32.Parse(ArrCount[i]);
                dictCount.Add(Int32.Parse(ArrID[i]), Int32.Parse(ArrCount[i]));
                dictPrice.Add(Int32.Parse(ArrID[i]), price);
            }

            Orders order = new Orders()
            {
                AppleOrders = new List<AppleOrders>(),
                Date = DateTime.Now.ToLongDateString(),
                Delivery = ord.Delivery,
                OrderNumber = number,
                Payment = ord.Payment,
                Status = "InProgress",
                Sum = sum,
                UserID = ord.UserID
            };
            unitOfWork.Orders.Add(order);
            await unitOfWork.CommitAsync();
            for(var i = 0; i <apple.Length;++i)
            {
                order.AppleOrders.Add(new AppleOrders() {
                    Apple = apple[i],
                    AppleID = apple[i].AppleID,
                    Count = Int32.Parse(ArrCount[i]),
                });
            }
            await unitOfWork.CommitAsync();
            var user = context.Users.Where(u => u.Id == order.UserID).FirstOrDefault();
            adminOrder.ChangeStatus(ord.OrdersID, "Removed");
            adminOrder.SendNewOrder(user, order);

            if(user.Email != "")
                await emailSender.SendOrder(apple, dictCount, dictPrice, user);
            return true;
        }

        [HttpPost]
        public IActionResult ChangeUserData(ApplicationUser user, String ReturnUrl = "/Admin/Main")
        {
            var usr = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            usr.Address = user.Address == null? "": user.Address;
            usr.City = user.City == null ? "" : user.City;
            usr.Email = user.Email == null ? "" : user.Email;
            usr.PhoneNumber = user.PhoneNumber == null ? "" : user.PhoneNumber;
            context.SaveChanges();
            return RedirectPermanent(ReturnUrl);
        }

        private String GetRoleName()
        {
            String role = "";
            if (User.IsInRole("SuperAdmin"))
                role = "SuperAdmin";
            else if (User.IsInRole("Admin"))
                role = "Admin";
            else
                role = "Client";
            return role;
        }

        //=============Goods===============================
        public IActionResult GoodsMain()
        {
            ViewData["Role"] = GetRoleName();
            return View();
        }

        public IActionResult ChangeElement(Int32 id)
        {
            HttpContext.Session.SetInt32("element",id);
            ViewData["Role"] = GetRoleName();
            return View();
        }

        public async Task<Apple> UpdateGoods(Apple apple)
        {
            if (apple == null)
                return null;
            return await updateGoods.Update(apple);
        }

        public async Task<IActionResult> RemoveElement(Int32 id)
        {
            //HttpContext.Session.SetString("element", id.ToString());
            ViewData["Role"] = GetRoleName();
            var apple = await unitOfWork.Apple.Find(a => a.AppleID == id);
            if (apple == null)
                return RedirectToAction("Error","Home","Объект с данным ключем не существует!");
            return View(apple);
        }

        public async Task<String> RemoveEl(Int32 id)
        {
            var apple = await unitOfWork.Apple.Find(a => a.AppleID == id);
            if (apple == null)
                return "Товар несуществует или удален";

            String name = apple.Model;
            unitOfWork.Apple.Remove(apple);
            var result = await unitOfWork.CommitAsync();

            if (result == 1)
                return "Товар " + name + " успешно удален";

            return "Произошла ошибка при удалении";
        }

        public IActionResult AddElement()
        {
            ViewData["Role"] = GetRoleName();
            HttpContext.Session.SetObjectAsJson("images", new List<String>());
            return View();
        }

        public async Task<Apple> GetElement()
        {
            var id = HttpContext.Session.GetInt32("element");
            if (id == null)
                return null;

            Int32 i = id ?? default(int);
            var apple = await unitOfWork.Apple.GetOneInclude(i);
            return apple;
        }

        public async Task<Apple> GetFirstElementInCategory(Int32 id)
        {
            var apple = await unitOfWork.Apple.GetFirstInclude(a => a.Categories.CategoriesID == id);
            apple.AppleImage = new List<Image>();
            apple.AppleColor = new List<AppleColor>();

            return apple;
        }

        public Image GetImageObject()
        {
            return new Image() {
                ColorID = 1,
                Apple = new Apple(),
                AppleID = 1,
                Path = "",
                Size = "large"
            };
        }

        public ProductDetails GetNewProductDetail()
        {
            return new ProductDetails()
            {
                DetailNames = new DetailNames(),
                Measure = "",
                Other = "",
                Value = "",
            };
        }

        public async Task<DetailNames[]> GetAllDetailNames()
        {
            var dnames = await unitOfWork.DetailNames.GetAll();
            return dnames.ToArray();
        }

        public async Task<String[]> GetAllDetailValues()
        {
            var details = await unitOfWork.ProductDetails.GetAll();
            IList<String> lst = new List<String>();
            foreach(var item in details)
            {
                if (!lst.Contains(item.Value))
                    lst.Add(item.Value);
            }
            return lst.ToArray();
        }

        public async Task<String> CreateGoods(Apple apple)
        {
            IList<String> imagesList = HttpContext.Session.GetObjectFromJson<IList<String>>("images");
            String appleUrl = await createGoods.CreateGoods(apple, imagesList);
            HttpContext.Session.SetObjectAsJson("images", new List<String>());
            return appleUrl;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(Int32 id)
        {
            var files = Request.Form.Files;
            IList<String> lst = HttpContext.Session.GetObjectFromJson<IList<String>>("images");
            var category = await unitOfWork.Categories.Find(cat => cat.CategoriesID == id);
            String categoryName = category.CategoryName;

            lst = createGoods.SaveImages(files, lst, categoryName);
            HttpContext.Session.SetObjectAsJson("images", lst);

            String response = $"{files.Count()} успешно сохранены";
            return new JsonResult(response);
        }

        public IActionResult UsersMain()
        {
            //var users = (context as DbContext).Set<ApplicationUser>().ToArray();
            return View();
        }

        public ApplicationUser[] GetAllUsers()
        {
            return (context as DbContext).Set<ApplicationUser>().Include(u => u.Roles).ToArray();
        }

        public String GetCurrentUserRole()
        {
            return User.IsInRole("SuperAdmin") ? "SuperAdmin" : User.IsInRole("Admin") ? "Admin" : "Client";
        }

        public IList<IdentityRole> GetAllRoles()
        {
            return context.Roles.ToList();
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<String> ChangeRole(String id, String roleId)
        {
            var user = context.Users.Where(u => u.Id == id).FirstOrDefault();
            var role = context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
            var admin = await userManager.IsInRoleAsync(user, "Admin");
            var super = await userManager.IsInRoleAsync(user, "SuperAdmin");
            
            //var key = await userManager.IsInRoleAsync(user, role.Name);
            if (role.Name == "SuperAdmin")
            {
                if (super)
                    return "ok";
                if (!admin)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                await userManager.AddToRoleAsync(user, "SuperAdmin");

            }
            else if(role.Name == "Admin")
            {
                if (admin)
                    return "ok";
                if (super)
                {
                    await userManager.RemoveFromRoleAsync(user, "SuperAdmin");
                }
                await userManager.AddToRoleAsync(user, "Admin");
            }
            else if(role.Name == "Client")
            {
                if (super)
                    await userManager.RemoveFromRoleAsync(user, "SuperAdmin");
                if (admin)
                    await userManager.RemoveFromRoleAsync(user, "Admin");

                var client = await userManager.IsInRoleAsync(user, "Client");
                if (!client)
                    await userManager.AddToRoleAsync(user, "Client");
            }
            context.SaveChanges();
            return "ok";
        }
    }

    
}
