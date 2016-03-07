using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using AppleStore.Models;
using AppleStore.Services;
using Store.Repository.UnitOfWorks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;
        
        public OrdersController(UserManager<ApplicationUser> userManager,
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

        [HttpPost]
        [Route("orders")]
        public IActionResult GetOrders()
        {

            return new ObjectResult("");
        }
    }
}
