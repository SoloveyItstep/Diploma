using AppleStore.DataServices.Hubs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleStore.Models;
using Store.Entity.Order;
using Microsoft.AspNet.SignalR;
using AppleStore.Services.Hubs;

namespace AppleStore.DataServices.Hubs.Facade
{
    public class OrderHubFacade : IOrderHubFacade
    {
        private readonly IHubContext hub;
        public OrderHubFacade()
        {
            hub = GlobalHost.ConnectionManager.GetHubContext<AdminOrdersHub>();
        }
        public void SendNewOrder(ApplicationUser user, Orders order)
        {
            hub.Clients.All.AddOrder(user,order);
        }
    }
}
