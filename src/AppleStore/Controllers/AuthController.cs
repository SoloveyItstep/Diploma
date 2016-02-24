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
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Web.ModelBinding;
using AppleStore.Models.RegisterLogin;
using Microsoft.AspNet.Identity.EntityFramework;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger logger;
        private readonly IEmailSender emailSender;
        private readonly IRegisterLoginErrorsLanguage errorsMessage;
        private readonly IMD5Hash md5;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ILoggerFactory loggerFactory,
                              IEmailSender emailSender,
                              IRegisterLoginErrorsLanguage errorsMessage,
                              IMD5Hash md5)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = loggerFactory.CreateLogger<AccountController>();
            this.emailSender = emailSender;
            this.errorsMessage = errorsMessage;
            this.md5 = md5;
        }

        [HttpPost]
        public async Task<Boolean> LogOut()
        {
            if (User.Identity.Name == null)
                return false;
            await signInManager.SignOutAsync();
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<String> Login(LoginViewModel model)
        {
            if (ModelState.IsValid && User.Identity.Name == null)
            {
                model.RememberMe = true;
                var user = userManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                var result = SignInResult.Success;

                if (user == null)
                    result = SignInResult.Failed;
                else
                {
                    PasswordVerificationResult virification = md5.VerifyHashedPassword(user, 
                        user.PasswordHash, model.Password);
                    if (user.Email == model.Email && virification == PasswordVerificationResult.Success)
                        result = SignInResult.Success;
                }

                if (result.Succeeded)
                {
                    logger.LogInformation(1, "User logged in.");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return "true";
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning(2, "User account locked out.");
                    return "true";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return "Invalid Email or Password";
                }
            }
            else if(User.Identity.Name != null)
            {
                //await signInManager.SignOutAsync();
                return "false";
            }
            return "false";
            
        }

        [ValidateAntiForgeryToken]
        public String Register(RegisterViewModel model,String returnUrl)
        {
            if (ModelState.IsValid && User.Identity.Name == null)
            {
                var us = userManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                if (us != null)
                {
                    return " - This Email is already registered";
                }
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.Phone.ToString()
                };

                String key = md5.HashPassword(user, model.Password);
                   
                user.PasswordHash = key;
                HttpContext.Session.SetObjectAsJson("UserPreRegister", user);
                HttpContext.Session.SetString("pass", model.Password);
                if (returnUrl == null || returnUrl == "/")
                    returnUrl = "/Home/Index";
                HttpContext.Session.SetString("url", returnUrl);
                emailSender.SendEmailToConfirm(model.Email, "", key, model.UserName, "1");
                return "true";
            }
            else if (User.Identity.Name != null)
            {
                signInManager.SignOutAsync();
                return "- User is already loged in.<br/> &nbsp;&nbsp; To register new User you have to loggedout first";
            }
            String language = HttpContext.Session.GetString("language");
            if(language == null)
            {
                HttpContext.Session.SetString("language", "EN");
                language = "EN";
            }
            String errors = errorsMessage.Registration(ViewData.ModelState.Values, language);
            return errors;
        }

        public async Task<IActionResult> ConfirmEmail(String id,String userID)
        {
            var user = HttpContext.Session.GetObjectFromJson<ApplicationUser>("UserPreRegister");
            if(user == null)
            {
                return RedirectToAction("Error", "Email was not confirmed");
            }
            var password = HttpContext.Session.GetString("pass");
            var keyExist = md5.VirifyHashadPasswords(user.PasswordHash, id);
                

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
                if (url != null)
                {
                    String[] arr = url.Split('/');
                    return RedirectToAction(arr[2],arr[1]);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AddErrors(result);
                return new ObjectResult("User already exist");
            }
            
            
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
