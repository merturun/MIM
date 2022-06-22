using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Compilation;

namespace MIM.Helper
{
    public class LanguageHelper
    {
        public static string GetString(string key)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(MvcApplication.language);
            return (Resources.lang.ResourceManager.GetString(key) == null) ? "String Key Not Found" : Resources.lang.ResourceManager.GetString(key);
        }
    }
}