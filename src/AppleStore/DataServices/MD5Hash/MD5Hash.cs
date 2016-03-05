using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;

namespace AppleStore.Models
{
    public class MD5Hash : IMD5Hash
    {
        public string HashPassword(ApplicationUser user, string password)
        {
            var bytes = Encoding.ASCII.GetBytes(user.Email + password + user.UserName[0].ToString());
            var pass = MD5.Create("MD5").ComputeHash(bytes);
            return pass.ToString();
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            String verifyPass = HashPassword(user, providedPassword);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            PasswordVerificationResult verify = comparer.Compare(verifyPass, hashedPassword) == 0 ? 
                PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
            return verify;
        }

        PasswordVerificationResult IMD5Hash.VirifyHashadPasswords(string password1, string password2)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            PasswordVerificationResult verify = comparer.Compare(password1, password2) == 0 ?
                PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
            return verify;
        }
        
    }
}
