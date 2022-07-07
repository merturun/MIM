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
            return View();
        }

        public void ChangeLanguage(string language)
        {
            MvcApplication.language = language;
            
        }

        [ChildActionOnly]
        public ActionResult QuickUser()
        {
            if(MIM.Models.User.current != null) return PartialView("QuickUser", MIM.Models.User.current);
            return PartialView("QuickUser");
        }

        [ChildActionOnly]
        public ActionResult LanguageChanger()
        {
            string[] languages = new string[2] { "tr", "en" };
            return PartialView("LanguageChanger",languages);
        }

        [ChildActionOnly]
        public ActionResult Aside()
        {
            return PartialView("Aside");
        }        
    }
}