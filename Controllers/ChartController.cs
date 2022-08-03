using MIM.Config;
using MIM.Helper;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIM.Controllers
{
    public class ChartController : Controller
    {

        private readonly MIMDBContext db = new MIMDBContext();

        // GET: Chart
        public JsonResult UsersWithTitlesBarChart()
        {
            var query = from t in db.Titles
                        join u in db.Users on t.TitleID equals u.TitleID into users
                        where t.OrganizationID == Organization.current.OrganizationID
                        select new
                        {
                            Title = t.Name,
                            UserCount = users.Count(),
                        };

            var colors = new List<string>();
            var random = new Random();
            for (int i = 0; i < db.Titles.Where(x=> x.OrganizationID == Organization.current.OrganizationID).Select(x => x.TitleID).ToArray().Length; i++)
            {
                colors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }

            List<Datasets> dataSet = new List<Datasets>
            {
                new Datasets()
                {
                    label = LanguageHelper.GetString("Chart.Title.UserCount"),
                    data = query.Select(a => a.UserCount).ToArray(),
                    backgroundColor = colors.ToArray(),
                    borderColor = colors.ToArray(),
                    borderWidth = "1"
                }
            };
            Chart chart = new Chart
            {
                labels = db.Titles.Where(x => x.OrganizationID == Organization.current.OrganizationID).Select(x => x.Name).ToArray(),
                datasets = dataSet
            };
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}