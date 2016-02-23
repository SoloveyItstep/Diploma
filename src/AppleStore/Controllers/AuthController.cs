using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using AppleStore.ViewModels.Account;
using AppleStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNet.Http;
using System.Web.Configuration;
using AppleStore.Services.MessageSender;
using AppleStore.Services;
using System.Web;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;
        private readonly IEmailSender emailSender;
        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ILoggerFactory loggerFactory,
                              IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = loggerFactory.CreateLogger<AccountController>();
            this.emailSender = emailSender;
        }


        public async Task<Boolean> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                logger.LogInformation(1, "User logged in.");
                return false;
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning(2, "User account locked out.");
                return true;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return false;
            }
       
            
        }

        //[ValidateAntiForgeryToken()]
        public Boolean Register(RegisterViewModel model,String returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {   UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.Phone.ToString()
                };
                
                String key = new MyPasswordHasher(FormsAuthPasswordFormat.MD5)
                    .HashPassword(model.Password);
                user.PasswordHash = key;
                HttpContext.Session.SetObjectAsJson("UserPreRegister", user);
                HttpContext.Session.SetString("pass", model.Password);
                //string url = HttpContext.Request.Path.ToUriComponent();
                HttpContext.Session.SetString("url", returnUrl);
                emailSender.SendEmailToConfirm(model.Email, "", key, model.UserName,"1");
                ViewData["ReturnUrl"] = returnUrl;
                return true;
            }
            return false;
        }


        public async Task<IActionResult> ConfirmEmail(String id,String userID)
        {
            var user = HttpContext.Session.GetObjectFromJson<ApplicationUser>("UserPreRegister");
            if(user == null)
            {
                return RedirectToAction("Error", "Email was not confirmed");
            }
            var hashedKey = HttpContext.Session.GetString("pass");
            var keyExist = new MyPasswordHasher(FormsAuthPasswordFormat.MD5)
                .VerifyPassword(id, hashedKey);

            if(keyExist != PasswordVerificationResult.Success)
            {
                return RedirectToAction("Error", "Password Incorrect");
            }
            var urlObj = HttpContext.Session.GetString("url");
            String url = String.Empty;
            if (urlObj != null)
                url = urlObj.ToString();
            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                logger.LogInformation(3, "User created a new account with password.");
                if(url != null)
                    return RedirectToLocal(url);
                return RedirectToLocal("/Home/Index");
            }
            AddErrors(result);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private IActionResult Error(String error)
        {
            return View(error);
        }
    }
}
