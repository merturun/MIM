using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Title
    {
        [Key]
        public int titleID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<User> users { get; set; }
    }
}