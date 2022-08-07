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
        public static User Current()
        {
            int userid = 0;
            if (HttpContext.Current.Session["current_userID"] != null)
                userid = ((int)HttpContext.Current.Session["current_userID"]);
            else
                return new User(true);
            
            MIMDBContext db = new MIMDBContext();
            return db.Users.Where(x => x.UserID == userid).First();
        }
        public User()
        {
            Groups = new List<Group>();
        }

        public User(bool isDefault=true)
        {
            Firstname = Lastname = Nickname = Username = Password = "Default";
            Email = "";
           
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
        public string AvatarUrl { get; set; }
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

        public string hasAvatar() { return ((AvatarUrl == null) || (AvatarUrl == "")) ? "noavatar.png" : AvatarUrl; }
        public bool isGranted(string[] controller) // Gelen controller'lardan herhangi bir metoda yetki varsa yetki ver
        {
            string[] wanted_grants = new string[] { "All", "Table" };
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

            
            isgranted = grants.Find(x => wanted_grants.Contains(x.Action)) != null;
            if (isgranted) return true; // Kullanıcı Belirtilen controller üzerinde All yetkisine sahipse yetki ver
            isgranted = grants.Find(x => controller.Contains(x.Controller)) != null;
            return isgranted;
        }

        public string GetShortName()
        {
            if (this.Firstname == " ") return "";
            return this.Firstname.Length > 15 ? this.Firstname.Substring(0, this.Firstname.Substring(0, 15)
                .LastIndexOf(" ")) + "..." : this.Firstname + ".";
        }

        public string GetShortSymbol()
        {
            if (this.fullname == " ") return "";
            string[] names = this.fullname.Split(' ');
            return names.First().Substring(0, 1) + names.Last().Substring(0, 1);
        }
        #endregion "Method + Properties"
    }

    public class UserValidator : AbstractValidator<MIM.Models.User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("İsim Boş olamaz");
        }
    }
}