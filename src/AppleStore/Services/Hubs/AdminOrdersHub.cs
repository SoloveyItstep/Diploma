using AppleStore.Models;
using Microsoft.AspNet.SignalR;
using Store.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Services.Hubs
{
    public class AdminOrdersHub: Hub
    {
        private readonly ApplicationDbContext context;
        public AdminOrdersHub(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SendNewOrder(ApplicationUser user, Orders order)
        {
            Clients.All.AddOrder(user,order);
        }

    }
}
