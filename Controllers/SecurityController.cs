using MIM.Config;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MIM.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var UserInDb = db.Users.FirstOrDefault(x => x.username == user.username && x.password == user.password);
            if (UserInDb != null)
            {
                FormsAuthentication.SetAuthCookie(UserInDb.username, false);
                MIM.Models.Organization.current = UserInDb.organization;
                MIM.Models.User.current = UserInDb;                    
                return RedirectToAction("Index", "Home");
            }
            else
            {                
                return View();
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}