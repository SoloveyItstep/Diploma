using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using AppleStore.Models;
using Microsoft.Extensions.CodeGeneration;
using AppleStore.Services;
using AppleStore.Models.RegisterLogin;
using Store.Repository.UnitOfWorks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public AdminController(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               IEmailSender emailSender,
                               ApplicationDbContext context,
                               IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Main()
        {
            String role = "";
            if (User.IsInRole("SuperAdmin"))
                role = "SuperAdmin";
            else if (User.IsInRole("Admin"))
                role = "Admin";
            else
                role = "Client";
            ViewData["Role"] = role;
            return View();
        }
    }
}
