using System;
using System.Globalization;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace MIM.Localization
{
    public static class ResourceExtensions
    {
        public static string Resource(this Controller controller, string expression, params object[] args)
        {
            ResourceExpressionFields fields = GetResourceFields(expression, "~/");
            return GetGlobalResource(fields, args);
        }
        public static string Resource(this HtmlHelper htmlHelper, string expression, params object[] args)
        {
            string path = (string)htmlHelper.ViewData[LocalizationWebFormView.ViewPathKey];
            if (string.IsNullOrEmpty(path))
                path = "~/";

            ResourceExpressionFields fields = GetResourceFields(expression, path);
            if (!string.IsNullOrEmpty(fields.ClassKey))
                return GetGlobalResource(fields, args);

            return GetLocalResource(path, fields, args);
        }
        public static string Language(this HtmlHelper htmlHelper, string controller, string key, params object[] args)
        {
            //Replace Language with a name of your choice, if you have lang.en.resx, you should change the value here to "lang"
            string expression;
            if (controller != "")
                expression = "lang" + controller + "," + key;
            else
                expression = "lang," + key;
            return Resource(htmlHelper, expression, args);
        }
        static string GetLocalResource(string path, ResourceExpressionFields fields, object[] args)
        {
            try
            {
                return string.Format((string)HttpContext.GetLocalResourceObject(path, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
            }
            catch { return (string)HttpContext.GetLocalResourceObject(path, fields.ResourceKey, CultureInfo.CurrentUICulture); }
        }
        static string GetGlobalResource(ResourceExpressionFields fields, object[] args)
        {
            try
            {
                return string.Format((string)HttpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, CultureInfo.CurrentUICulture), args);
            }
            catch { return (string)HttpContext.GetGlobalResourceObject(fields.ClassKey, fields.ResourceKey, CultureInfo.CurrentUICulture); }
        }
        static ResourceExpressionFields GetResourceFields(string expression, string virtualPath)
        {
            var context = new ExpressionBuilderContext(virtualPath);
            var builder = new ResourceExpressionBuilder();
            return (ResourceExpressionFields)builder.ParseExpression(expression, typeof(string), context);
        }
    }
}