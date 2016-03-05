using AppleStore.Models;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailToConfirm(String email, String subject, String key, String userName, String userID);

        Task SendOrder(Apple[] apple, Dictionary<Int32, Int32> count, Dictionary<Int32,Decimal> price, ApplicationUser user);
    }
}
