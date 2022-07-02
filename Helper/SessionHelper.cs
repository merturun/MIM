using MIM.Config;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIM.Helper
{
    public static class SessionHelper
    {
        public static MIMDBContext db = new MIMDBContext();
        public static User currentuser;
        public static Organization currentorganization;
        public static void login(User user)
        {
            HttpContext.Current.Session["userID"] = user.userID;
            HttpContext.Current.Session["organizationID"] = user.organizationID;
        }

        public static User current_user()
        {
            if (HttpContext.Current.Session["userID"] != null)
                return db.Users.Find(HttpContext.Current.Session["userID"]);
            else
                return null;
        }

        public static Organization current_organization()
        {
            if (HttpContext.Current.Session["organizationID"] != null)
                return db.Organizations.Find(HttpContext.Current.Session["organizationID"]);
            else
                return null;
        }

        public static void set_current_user()
        {
            if (HttpContext.Current.Session["userID"] != null) User.current = current_user();
        }

        public static void set_current_organization()
        {
            if (HttpContext.Current.Session["organizationID"] != null) Organization.current = current_organization();
        }

        public static bool logged_in()
        {
            return current_user() != null;
        }

        public static void logout(User user)
        {
            HttpContext.Current.Session.Remove("userID");
            HttpContext.Current.Session.Remove("organizationID");
            currentuser = null;
            currentorganization = null;
        }

        //  def store_location
        //    session[:forwarding_url] = request.url if request.get? &&
        //      request.format ! = :json
        //  end
    }
}