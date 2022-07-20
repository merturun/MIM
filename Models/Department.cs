using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UserID { get; set; }
        public virtual User User { get; set; }
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }

        public override string ToString() { return Name; }
    }
}