using MIM.Config;
using MIM.Localization;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MIM.Controllers
{
    public class HomeController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        public ActionResult Index()
        {
            User user = db.Users.FirstOrDefault();
            if (MvcApplication.current_user == null) return RedirectToAction("Login", "Home");
            return View();
        }

        [Localization]
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public void ChangeLanguage(string language)
        {
            MvcApplication.language = language;
        }

        [ChildActionOnly]
        public ActionResult QuickUser()
        {
            if(MvcApplication.current_user != null) return PartialView("QuickUser",MvcApplication.current_user);
            return PartialView("QuickUser");
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var UserInDb = db.Users.FirstOrDefault(x => x.username == user.username && x.password == user.password);
            if (UserInDb != null)
            {
                MvcApplication.current_user = UserInDb;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.mesaj = "Giriş yapamadın ak";
                return View();
            }
        }
    }
}