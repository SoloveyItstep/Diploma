using AppleStore.Models;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace AppleStore.Services.MessageSender
{
    public class MessageService: IEmailSender,ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mess = new MailMessage();
            mess.To.Add(new MailAddress(email, "User"));
            mess.From = new MailAddress("solovey.itstep@gmail.com", "Vladimir");
            mess.Subject = subject;
            String url = "<a href='https://localhost:44318/Auth/ConfirmEmail/" + subject + "' rel='link' title='Confirm Email'>Link!</a>";
            string html = @"<p>html body</p>" + url;
            mess.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Plain));
            mess.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("solovey.itstep@gmail.com", "solovey14060");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mess);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
                throw new Exception(ex.Message);
            }
            
            return Task.FromResult(0);
        }

        public Task SendOrderToUser(Apple[] apple, Dictionary<Int32, Int32> count, Dictionary<Int32, Decimal> price, 
            ApplicationUser user, String orderNumber)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));
            message.From = new MailAddress("solovey.itstep@gmail.com", "VS");

            String html = BuildUserOrder(apple, count, price, user,orderNumber);
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("New order", null, MediaTypeNames.Text.Plain));
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                                                  //add smtp (example - smtp.gmail.com) and port (example - 587)
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                                                                                        //add email and password
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("solovey.itstep@gmail.com", "solovey14060");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
                throw new Exception(ex.Message);
            }

            return Task.FromResult(0);
        }

        public Task SendEmailToConfirm(String email, String subject, String key, String userName,String userID)
        {
            MailMessage mess = new MailMessage();
            mess.To.Add(new MailAddress(email,userName));
            mess.From = new MailAddress("solovey.itstep@gmail.com", "VS Shop");
            mess.Subject = subject;
            String message = "Please Confirm email to finish registration";
            String url = "<a href='http://localhost:3923/Auth/ConfirmEmail/" + key +"?userID=" +userID+"' rel='link' title='Confirm Email'>Link!</a>";
            String html = @"<p>Hello "+userName+"</p></br><br/>" + url;
            mess.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Plain));
            mess.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("solovey.itstep@gmail.com", "solovey14060");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mess);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
                throw new Exception(ex.Message);
            }

            return Task.FromResult(0);
        }

        public Task SendOrder(Apple[] apple, Dictionary<int, int> count, Dictionary<Int32,Decimal> price, ApplicationUser user)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress("solovey.itstep@gmail.com"));
            message.From = new MailAddress("solovey.itstep@gmail.com", "VS Shop");

            String html = BuildOrder(apple, count, price, user);

            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("New order", null, MediaTypeNames.Text.Plain));
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("solovey.itstep@gmail.com", "solovey14060");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                String error = ex.Message;
                throw new Exception(ex.Message);
            }

            return Task.FromResult(0);

        }

        private String BuildUserOrder(Apple[] apple, Dictionary<Int32, Int32> count, Dictionary<Int32, Decimal> price, 
            ApplicationUser user, String orderNumber)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css' integrity='sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7' crossorigin='anonymous'><br/>");

            builder.Append("<div><br/><p>New Order From " + user.UserName + "</p><br/></div>");
            builder.Append("<div><b>Your order number - "+orderNumber+"</b><br/></div>");
            builder.Append("<div><br/><p>Address " + user.City + " " + user.Address + "</p><br/></div>");
            builder.Append("<div><br/><p>Email " + user.Email + "</p><br/></div>");
            builder.Append("<div><br/><p>Phone " + user.PhoneNumber + "</p><br/></div>");

            builder.Append("<div class='col-xs-12'>");
            foreach (var i in apple)
            {
                builder.Append("<div>" + i.Model + " - Count: " + count[i.AppleID]);
                builder.Append(" - Price: " + price[i.AppleID] + "</div>");
            }

            builder.Append("</div><br/><br/><div class='col-xs-12'>");

            Decimal p = 0;
            foreach (var pr in price.Values)
                p += pr;

            builder.Append("Total Price: " + p.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            builder.Append("</div>");

            return builder.ToString();
        }

        private String BuildOrder(Apple[] apple, Dictionary<Int32,Int32> count, Dictionary<Int32,Decimal> price, ApplicationUser user)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css' integrity='sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7' crossorigin='anonymous'><br/>");

            builder.Append("<div><br/><p>New Order From " + user.UserName + "</p><br/></div>");
            builder.Append("<div><br/><p>Address " +user.City+" "+ user.Address + "</p><br/></div>");
            builder.Append("<div><br/><p>Email " + user.Email + "</p><br/></div>");
            builder.Append("<div><br/><p>Phone " + user.PhoneNumber + "</p><br/></div>");

            builder.Append("<div class='col-xs-12'>");
            foreach(var i in apple)
            {
                builder.Append("<div>" + i.Model + " - Count: " + count[i.AppleID]);
                builder.Append(" - Price: " + price[i.AppleID] + "</div>");
            }

            builder.Append("</div><br/><br/><div class='col-xs-12'>");

            Decimal p = 0;
            foreach(var pr in price.Values)
                p += pr;

            builder.Append("Total Price: " + p.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            builder.Append("</div>");

            return builder.ToString();
        }

        public AuthMessageSenderOptions MailOptions
        {
            get
            {
                return new AuthMessageSenderOptions()
                {
                    SendGridKey = "solovey14060",
                    SendGridUser = "felex14060"
                };
            }
        }

        public AuthMessageSMSSenderOptions Options { get { return new AuthMessageSMSSenderOptions(); } }

        public Task SendSmsAsync(string number, string message)
        {
            var twilio = new TwilioRestClient(
            Options.SID,
            Options.AuthToken);
            var result = twilio.SendMessage(Options.SendNumber, number, message);
            return Task.FromResult(0);
        }
    }

    

}
