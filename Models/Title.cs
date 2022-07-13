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
        public Title()
        {
            Users = new List<User>();
        }
        public int TitleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizationID { get; set; } 
        public Organization Organization { get; set; }
        public List<User> Users { get; set; }
    }
}