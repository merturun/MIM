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
            var UserInDb = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (UserInDb != null)
            {
                FormsAuthentication.SetAuthCookie(UserInDb.Username, false);
                MIM.Models.Organization.current = UserInDb.Organization;
                MIM.Models.User.current = UserInDb;                    
                return RedirectToAction("Index", "Home");
            }
            else
            {                
                return View();
            }
        }

        public void ChangeLanguage(string language)
        {
            MvcApplication.language = language;
        }

        [ChildActionOnly]
        public ActionResult LanguageChanger()
        {
            string[] languages = new string[2] { "tr", "en" };
            return PartialView("LanguageChanger", languages);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}