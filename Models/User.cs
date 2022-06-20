using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class User
    {
        public User()
        {
            this.groups = new HashSet<Group>();
        }

        [Key]
        public int userID { get; set; }
        public int organizationID { get; set; }   
        public virtual Organization organization { get; set; }
        public int titleID { get; set; }
        public virtual Title title { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string nickname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public DateTime bornDate { get; set; }
        public bool superAdmin { get; set; }
        public virtual ICollection<Group> groups { get; set; }

        public string FullName { get { return firstname + " " + lastname; } }
    }
}