using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Organization Parent { get; set; }
        public virtual ICollection<Organization> Childs { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
    }
}