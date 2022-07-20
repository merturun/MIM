using FluentValidation;
using MIM.Config;
using MIM.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MIM.Models
{
    public class User
    {
        public static User current = new User(true);

        public User()
        {
            Groups = new List<Group>();
        }
        public User(bool isDefault=true)
        {
            Firstname = Lastname = Nickname = Username = Password = "Default";
            Email = "Default@default.com";
           
            Title = new Title() { Name = "Default" };
           
        }

        #region "Model Properties"
        public int UserID { get; set; }
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }
        public int? TitleID { get; set; }
        public virtual Title Title { get; set; }
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BornDate { get; set; }
        public bool SuperAdmin { get; set; }
        public virtual ICollection<Group> Groups { get; set; }



        #endregion "Model Properties"

        #region "Method + Properties"
        public string fullname { get { return Firstname + " " + Lastname; } }
        public string symbol { get { string[] test = fullname.Split(' '); string sembol = ""; foreach (string item in test) { sembol += item.Substring(0, 1); } return sembol.ToUpper(); } }
        public bool isGranted(string action, string controller) // Belirtilen controller ve actiona göre yetki ver
        {
            bool isgranted = false;
            MIMDBContext db = new MIMDBContext();
            User user = db.Users.Find(UserID);
            if (user == null) return true;
            if (user.SuperAdmin) isgranted = true;
            if (isgranted) return true; // Kullanıcı Süper Adminse yetki ver
            List<Grant> grants = new List<Grant>();
            foreach (Group grp in user.Groups)            
                foreach (Grant grt in grp.Grants)                
                    grants.Add(grt);

            isgranted = grants.Find(x => x.Action == "All" && x.Controller == controller) != null;
            if (isgranted) return true; // Kullanıcı Belirtilen controller üzerinde All yetkisine sahipse yetki ver
            isgranted = grants.Find(x => x.Action == action && x.Controller == controller) != null;
            return isgranted;
        }
        public bool isGranted(string[] controller) // Gelen controller'lardan herhangi bir metoda yetki varsa yetki ver
        {
            bool isgranted = false;
            MIMDBContext db = new MIMDBContext();
            User user = db.Users.Find(UserID);
            if (user == null) return true;
            if (user.SuperAdmin) isgranted = true;
            if (isgranted) return true; // Kullanıcı Süper Adminse yetki ver
            List<Grant> grants = new List<Grant>();
            foreach (Group grp in user.Groups)
                foreach (Grant grt in grp.Grants)
                    grants.Add(grt);

            
            isgranted = grants.Find(x => controller.Contains(x.Controller)) != null;
            if (isgranted) return true; // Kullanıcı Belirtilen controller üzerinde All yetkisine sahipse yetki ver
            isgranted = grants.Find(x => controller.Contains(x.Controller)) != null;
            return isgranted;
        }
        #endregion "Method + Properties"
    }
}