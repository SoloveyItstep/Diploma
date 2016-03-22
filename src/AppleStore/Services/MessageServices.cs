using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleStore.Models;
using Store.Entity;

namespace AppleStore.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public Task SendEmailToConfirm(string email, string subject, string key, string userName,String userID)
        {
            throw new NotImplementedException();
        }

        public Task SendOrder(Apple[] apple, Dictionary<int, int> count, Dictionary<int, decimal> price, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SendOrderToUser(Apple[] apple, Dictionary<int, int> count, Dictionary<int, decimal> price, ApplicationUser user, string orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
