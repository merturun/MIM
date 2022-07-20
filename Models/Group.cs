using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual List<Grant> Grants { get; set; }
        public virtual List<User> Users { get; set; }

        public override string ToString() { return Name; }
    }
}