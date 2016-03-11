using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using AppleStore.ViewModels.Account;
using AppleStore.Models;
using Store.Repository.UnitOfWorks;
using Microsoft.AspNet.Identity;
using AppleStore.Services;
using AppleStore.DataServices.Cart.Interfaces;
using AppleStore.DataServices.Hubs.Interfaces;
using Store.Entity.Order;
using Store.Entity;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailSender email;
        private readonly ICart cart;
        private readonly IOrderHubFacade adminOrder;

        public CartController(ApplicationDbContext context, 
                              IUnitOfWork unitOfWork,
                              IEmailSender email,
                              ICart cart, 
                              IOrderHubFacade adminOrder)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
            this.email = email;
            this.cart = cart;
            this.adminOrder = adminOrder;
        }

        public IActionResult Index()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == "EN" || language == null)
                return View("Cart.en-US");
            return View("Cart.ru-RU");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Boolean> PlaceAnOrder(LoginNewUserOrderViewModel id)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.Where(u => u.UserName == id.UserName && u.PhoneNumber == id.Phone).FirstOrDefault();
                if (User.IsInRole("Client") || User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
                {
                    
                }
                else
                {
                    if (user == null)
                    {
                        user = new ApplicationUser();
                        user.Address = (id.Address != null ? id.Address : "");
                        user.City = (id.City != null ? id.City : "");
                        user.Email = (id.Email != null ? id.Email : "");
                        user.PhoneNumber = (id.Phone != null ? id.Phone : "");
                        user.UserName = (id.UserName != null ? id.UserName : "");

                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                cart.GetHttpContext(HttpContext);
                
                    var count = cart.GetCounts();
                var apple = await unitOfWork.GetCartData(count);
                var price = await cart.GetPrice();
                    await email.SendOrder(apple, count, price, user);

                    Decimal sum = 0;
                    foreach(var i in count.Keys)
                    {
                        var pr = apple.Where(a => a.AppleID == i).FirstOrDefault().Price;
                        sum += (pr * count[i]);
                    }
                        

                    string num = unitOfWork.Orders.GetLastOrderNumber();
                    if (num == null)
                        num = "0000001";

                    Int32 n = Int32.Parse(num);
                    n++;
                    num = n.ToString();
                Int32 length = num.Length;
                    for(var i = 0; i < 8 - length; ++i)
                    {
                        num = "0" + num;
                    }

                    Orders order = new Orders()
                    {
                        Date = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
                        Delivery = id.Delivery,
                        Payment = id.Payment,
                        Status = "New",
                        Sum = sum,
                        OrderNumber = num,
                        UserID = user.Id,
                        AppleOrders = new List<AppleOrders>()
                    };

                unitOfWork.Orders.Add(order);
                await unitOfWork.CommitAsync();
                order = await unitOfWork.Orders.GetByOrderId(order.OrderNumber);

                foreach (var ao in apple)
                {
                    var a = new AppleOrders()
                    {
                        Apple = ao,
                        AppleID = ao.AppleID,
                        Count = count[ao.AppleID],
                        OrdersID = order.OrdersID,
                        Orders = order
                    };
                    unitOfWork.AppleOrders.Add(a);
                    unitOfWork.Commit();
                }
                
                order = await unitOfWork.Orders.GetByOrderId(num);
                adminOrder.SendNewOrder(user,order);
                
                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<Orders[]> GetOrders()
        {
            var orders = await unitOfWork.Orders.GetAllOrdersInclude();
            orders = orders.OrderByDescending(o => o.OrderNumber).ToArray();
            return orders;
        }

        [HttpPost]
        public async Task<ApplicationUser[]> GetOrderUsers()
        {
            var list = new List<ApplicationUser>();
            var orders = await unitOfWork.Orders.GetAllOrdersInclude();
            orders = orders.OrderByDescending(o => o.OrderNumber).ToArray();

            foreach (var ord in orders)
            {
                list.Add(context.Users.Where(u => u.Id == ord.UserID).FirstOrDefault());
            }
            return list.ToArray();
        }

        [HttpPost]
        public async Task ChangeOrderStatus(Int32 OrderID,String Status)
        {
            var order = await unitOfWork.Orders.Find(ord => ord.OrdersID == OrderID);
            order.Status = Status;
            await unitOfWork.CommitAsync();
            order = await unitOfWork.Orders.Find(ord => ord.OrdersID == OrderID);
            adminOrder.ChangeStatus(OrderID, Status);
        }
    }
}
