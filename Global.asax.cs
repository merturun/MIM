using MIM.Config;
using MIM.Helper;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MIM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static MIMDBContext db = new MIMDBContext();
        public static int userID;
        public static int organizationID;
        public static User current_user { get { db = new MIMDBContext(); return db.Users.FirstOrDefault(x => x.userID == userID); } }
        public static Organization current_organization { get { db = new MIMDBContext(); return db.Organizations.FirstOrDefault(x => x.organizationID == organizationID); } }
        public static string language = "tr";
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


    }
}
