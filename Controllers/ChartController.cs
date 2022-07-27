using MIM.Config;
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

        private MIMDBContext db = new MIMDBContext();

        // GET: Chart
        public JsonResult UsersWithTitlesBarChart()
        {
            var query = from t in db.Titles
                        join u in db.Users on t.TitleID equals u.TitleID into users
                        select new
                        {
                            Title = t,
                            UserCount = users.Count(),
                        };
            
            List<Datasets> dataSet = new List<Datasets>
            {
                new Datasets()
                {
                    label = "User Count",
                    data = query.Select(a => a.UserCount).ToArray(),
                    backgroundColor = new string[] { "#FFFFFF", "#000000", "#FF00000" },
                    borderColor = new string[] { "#FFFFFF", "#000000", "#FF00000" },
                    borderWidth = "1"
                }
            };
            Chart chart = new Chart
            {
                labels = db.Titles.Select(x => x.Name).ToArray(),
                datasets = dataSet
            };
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}