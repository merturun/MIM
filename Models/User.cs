using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class User
    {
        public User()
        {
            this.Groups = new HashSet<Group>();
        }

        [Key]
        public int Id { get; set; }
        public Organization Organization { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Username { get; set; }
        public Title Title { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime BornDate { get; set; }
        public bool SuperAdmin { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}