using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Models
{
    public interface IMD5Hash: IPasswordHasher<ApplicationUser>
    {
        PasswordVerificationResult VirifyHashadPasswords(String password1, String password2);
    }
}
