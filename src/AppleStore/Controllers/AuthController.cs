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
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Rendering;
using System.Web.Security;

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
        private readonly ApplicationDbContext context;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              ILoggerFactory loggerFactory,
                              IEmailSender emailSender,
                              IRegisterLoginErrorsLanguage errorsMessage,
                              IMD5Hash md5,
                              ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = loggerFactory.CreateLogger<AccountController>();
            this.emailSender = emailSender;
            this.errorsMessage = errorsMessage;
            this.md5 = md5;
            this.context = context;
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
        public async Task<String> Login(LoginViewModel model, String ReturnUrl = null)
        {
            if (ModelState.IsValid && User.Identity.Name == null)
            {
                model.RememberMe = true;
                var user = userManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                var result = CanLogin(user, model);
                if (ReturnUrl != null)
                    RedirectToLocal(ReturnUrl);
                return await LoginMethod(user, result);                
            }
            return "false";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginViewModel model, String ReturnUrl = null)
        {
            if (ModelState.IsValid && User.Identity.Name == null)
            {
                model.RememberMe = true;
                var user = userManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                var result = CanLogin(user, model);
                if (ReturnUrl != null)
                {
                    var v = await LoginMethod(user, result);
                    return RedirectToLocal(ReturnUrl);
                }
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View("Login-En",new LoginViewModel());
        }

        [ValidateAntiForgeryToken]
        public String Register(RegisterViewModel model,String returnUrl)
        {
            if (ModelState.IsValid && User.Identity.Name == null)
            {
                var user = CreateUser(model);
                if (user == null)
                    return " - This Email is already registered";
                SaveDataToSession(user, returnUrl, model);
                emailSender.SendEmailToConfirm(model.Email, "", user.PasswordHash, model.UserName, "1");
                return "true";
            }
            else if (User.Identity.Name != null)
                return "- User is already loged in.<br/> &nbsp;&nbsp; To register new User you have to loggedout first";

            return CreateRegistrationErrors();
        }

        public async Task<IActionResult> ConfirmEmail(String id,String userID)
        {
            var user = HttpContext.Session.GetObjectFromJson<ApplicationUser>("UserPreRegister");
            if(user == null)
                return RedirectToAction("Error", "Email was not confirmed");
            var password = HttpContext.Session.GetString("pass");
            var keyExist = md5.VirifyHashadPasswords(user.PasswordHash, id);
                
            if(keyExist != PasswordVerificationResult.Success)
                return RedirectToAction("Error", "Password Incorrect");
            
            var result = await userManager.CreateAsync(user);
            
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                logger.LogInformation(3, "User created a new account with password.");
                IdentityResult res = await userManager.AddToRoleAsync(user, "Client");
                
                String[] url = GetReturnUrl();
                return RedirectToAction(url[0],url[1]);
            }
                AddErrors(result);
                return new ObjectResult("User already exist");
        }

        private String CreateRegistrationErrors()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == null)
            {
                HttpContext.Session.SetString("language", "EN");
                language = "EN";
            }
            String errors = errorsMessage.Registration(ViewData.ModelState.Values, language);
            return errors;
        }

        private async Task<String> LoginMethod(ApplicationUser user,SignInResult result)
        {
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

        private SignInResult CanLogin(ApplicationUser user, LoginViewModel model)
        {
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
            return result;
        }

        private void SaveDataToSession(ApplicationUser user, String returnUrl, RegisterViewModel model)
        {
            HttpContext.Session.SetObjectAsJson("UserPreRegister", user);
            HttpContext.Session.SetString("pass", model.Password);
            if (returnUrl == null || returnUrl == "/")
                returnUrl = "/Home/Index";
            HttpContext.Session.SetString("url", returnUrl);
        }

        private String[] GetReturnUrl()
        {
            var urlObj = HttpContext.Session.GetString("url");
            String url = String.Empty;
            if (urlObj != null)
                url = urlObj.ToString();
            String[] arr = { "Index", "Home" };
            String[] ar = {"",""};
            if (url != null)
            {
                ar = url.Split('/');
                arr[0] = ar[2];
                arr[1] = ar[1];
            }
            return arr;
        }

        private ApplicationUser CreateUser(RegisterViewModel model)
        {
            var us = userManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();
            if (us != null)
                return null;
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
            return user;
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





        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        //
        // GET: /Auth/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            String username = info.ExternalPrincipal.Claims.ToArray()[1].Value;
            if(username == null)
                username = info.ExternalPrincipal.Claims.ToArray()[2].Value;
            HttpContext.Session.SetString("username", username);
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.ExternalPrincipal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new LoginViewModel() { Email = email });
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            }
        }

        //
        // POST: /Auth/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, 
                                                                    string returnUrl = null)
        {
            if (User.IsSignedIn())
            {
                await signInManager.SignOutAsync();
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                String tmp = HttpContext.Session.GetString("username");
                String username = "";
                foreach(Char ch in tmp)
                {
                    if (Char.IsLetter(ch) || Char.IsDigit(ch)
                        || ch == '-' || ch == '@' || ch == '(' || ch == ')'
                         || ch == '.')
                        username += ch.ToString();
                    else if (ch == ' ')
                        username += "-";
                }

                var user = new ApplicationUser { UserName = username, Email = model.Email, Address = "", City = "", PasswordHash = "", PhoneNumber = "" };
                var result = await userManager.CreateAsync(user);
                
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/SendCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var userFactors = await userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            // Generate the token and send it
            var code = await userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View("Error");
            }

            var message = "Your security code is: " + code;
            if (model.SelectedProvider == "Email")
            {
                await emailSender.SendEmailAsync(await userManager.GetEmailAsync(user), "Security Code", message);
            }
            //else if (model.SelectedProvider == "Phone")
            //{
            //    await smsSender.SendSmsAsync(await userManager.GetPhoneNumberAsync(user), message);
            //}

            return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/VerifyCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
                return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                logger.LogWarning(7, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError("", "Invalid code.");
                return View(model);
            }
        }

    }
}
