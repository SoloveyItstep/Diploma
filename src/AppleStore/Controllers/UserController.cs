using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AppleStore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [Route("currentlanguage")]
        public String GetCurrentLanguage()
        {
            String language = HttpContext.Session.GetString("language");
            if (language == null)
            {
                HttpContext.Session.SetString("language", "EN");
                language = "EN";
            }
            return language;
        }

        [Route("changelanguage")]
        public void ChangeLanguage(String id)
        {
            String language = HttpContext.Session.GetString("language");
            if (language == null)
                HttpContext.Session.SetString("language", "EN");
            else if (language == "EN")
                HttpContext.Session.SetString("language", "RU");
            else
                HttpContext.Session.SetString("language", "EN");
        }

        [Route("currentuser")]
        public String CurrentUser()
        {
            return User.Identity.Name;
        }
    }
}
