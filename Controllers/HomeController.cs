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
        [HttpPost]
        public ActionResult Login(User user)
        {
            var UserInDb = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (UserInDb != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("About", "Home");
            }
            else
            {
                ViewBag.mesaj = "Giriş yapamadın ak";
                return View();
            }
        }
    }
}