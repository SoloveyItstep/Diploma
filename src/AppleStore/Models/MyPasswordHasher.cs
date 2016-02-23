using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;

namespace AppleStore.Models
{
    public class MyPasswordHasher : Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>
    {

        public FormsAuthPasswordFormat FormsAuthPasswordFormat { get; set; }

        public MyPasswordHasher(FormsAuthPasswordFormat format)
        {
            FormsAuthPasswordFormat = format;
        }

        public string HashPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.ToString());
            //return null;
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            var testHash = FormsAuthentication.HashPasswordForStoringInConfigFile(providedPassword, FormsAuthPasswordFormat.ToString());
            return hashedPassword.Equals(testHash) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
            //return PasswordVerificationResult.Success;
        }
    }
}
