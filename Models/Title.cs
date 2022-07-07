using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<User> users { get; set; }
        public int? organizationID { get; set; }
        public Organization organization { get; set; }
    }
}