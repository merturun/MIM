using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIM
{
    public class ApplicationHelper
    {
        public string GetUserName()
        {
            if (MvcApplication.current_user == null) return "Test";
            return MvcApplication.current_user.firstname;
        }
    }
}