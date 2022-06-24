using MIM.Config;
using MIM.Helper;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
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

        [ChildActionOnly]
        public ActionResult LanguageChanger()
        {
            string[] languages = new string[2] { "tr", "en" };
            return PartialView("LanguageChanger",languages);
        }

        public JavaScriptResult TestDeneme()
        {
            return JavaScript("window.alert('testdeneme')");
        }

        public JavaScriptResult ModalCreator(string size,string controller, string action,bool isSub)
        {
            string js;
            js = "";
            return null;
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var UserInDb = db.Users.FirstOrDefault(x => x.username == user.username && x.password == user.password);
            if (UserInDb != null)
            {
                MvcApplication.current_user = UserInDb;
                MvcApplication.current_organization = UserInDb.organization;
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