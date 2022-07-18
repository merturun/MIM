using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIM.Helper
{
    public class HtmlObjectHelper
    {
        public static string IsExistLabel<T>(T obj, string type,string label)
        {
            string tmp = "";
            if (obj == null)
                tmp = "<span class='label label-lg label-light-" + type + " label-inline'>" + LanguageHelper.GetString(label) + "</span>";
            else
                tmp = obj.ToString();

            return tmp;
        }
    }
}