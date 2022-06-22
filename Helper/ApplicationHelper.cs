using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIM
{
    public class ApplicationHelper
    {
        public static string GetUserName()
        {
            if (MvcApplication.current_user == null) return "UN";
            return MvcApplication.current_user.firstname;
        }

        public static string GetUserSymbol()
        {
            if (MvcApplication.current_user == null) return "UN";
            return (MvcApplication.current_user.firstname.Substring(0,1) + MvcApplication.current_user.lastname.Substring(0, 1)).ToUpper();
        }
    }
}