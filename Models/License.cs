using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class License
    {
        public int LicenseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizationID { get; set; }

        public virtual Organization Organization { get; set; }

        public List<Module> Modules { get; set; }
    }
}