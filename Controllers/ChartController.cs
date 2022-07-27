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
            var userData = db.Users.ToList();
            var titleData = db.Titles.ToList();
            var usersPerTitle = from user in userData
                                
                                group user by user.TitleID;

            Chart chart = new Chart();
            chart.labels = userData.Select(x=>x.Title.Name).ToArray();
            chart.datasets = new List<Datasets>();
            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = "User Count",
                data = usersPerTitle,
                backgroundColor = new string[] { "#FFFFFF", "#000000", "#FF00000" },
                borderColor = new string[] { "#FFFFFF", "#000000", "#FF00000" },
                borderWidth = "1"
            });
            chart.datasets = dataSet;

            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}