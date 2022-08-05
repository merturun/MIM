using MIM.Config;
using MIM.Helper;
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
            var UserInDb = db.Users.FirstOrDefault(x => x.Username == user.Username);
            bool pValid = UserInDb !=null ? UserInDb.Password == user.Password : false;
            if (UserInDb != null && pValid) // Kullanıcı adı ve parola doğruysa
            {
                FormsAuthentication.SetAuthCookie(UserInDb.Username, false);
                Session["current_organizationID"] = UserInDb.OrganizationID;
                Session["current_userID"] = UserInDb.UserID;

                int userid = ((int)System.Web.HttpContext.Current.Session["current_userID"]);
                return RedirectToAction("Index", "Home");
            }
            else if (UserInDb != null && !pValid) // Kullanıcı adı doğru ama parola yanlış
            {
                ViewBag.Message = LanguageHelper.GetString("Login.Error.Password");

            }
            else // Kullanıcı bulunumadı
            {
                ViewBag.Message = LanguageHelper.GetString("Login.Error.NoUser");
            }
            return View();
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
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}