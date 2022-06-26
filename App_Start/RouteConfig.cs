using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "UserModal",
            //    url: "Users/{action}/{type}/{id}",
            //    defaults: new { controller = "Users", action = "Modal", type = UrlParameter.Optional, id = UrlParameter.Optional }
            //    );

            //routes.MapRoute(
            //    name: "User",
            //    url: "users",
            //    defaults: new { controller = "Users", action = "Index" }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );            
        }
    }
}
