using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //public static string MultipleDropDownList<T>(IEnumerable<SelectListItem> list,T obj,string objectid, string first)
        //{
        //    string tmp = "";
        //    string a = "", b = "";
        //    tmp += " <select id='" + objectid + "' multiple='multiple' name='" + objectid + "'>";

        //    foreach (SelectListItem item in list)
        //    {
        //        tmp += "<option selected='selected'value='" + item.Value + "'>" + item.Text + "</option>";
        //    }
            
        //    tmp += "</ select>";
        //    return tmp;


        //}
    }
}