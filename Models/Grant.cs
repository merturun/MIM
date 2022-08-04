using MIM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Grant
    {
        public int GrantID { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public List<Group> Groups { get; set; }
        public string Name { get { return LanguageHelper.GetString(string.Format("{0}->{1}", Controller, Action)); }}
    }
}