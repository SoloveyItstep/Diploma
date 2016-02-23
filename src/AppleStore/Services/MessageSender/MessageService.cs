using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
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

        public Task SendEmailToConfirm(String email, String subject, String key, String userName,String userID)
        {
            MailMessage mess = new MailMessage();
            mess.To.Add(new MailAddress(email,userName));
            mess.From = new MailAddress("solovey.itstep@gmail.com", "Vladimir");
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
