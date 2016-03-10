using AppleStore.Models;
using Store.Entity;
using Store.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Hubs.Interfaces
{
    public interface IOrderHubFacade
    {
        void SendNewOrder(ApplicationUser user, Orders order);
        
    }
}
