using FluentValidation;
using MIM.Helper;
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
        public List<Group> Groups { get; set; }

      

        #endregion "Model Properties"

        #region "Method + Properties"
        public string fullname { get { return Firstname + " " + Lastname; } }
        public string symbol { get { string[] test = fullname.Split(' '); string sembol = ""; foreach (string item in test) { sembol += item.Substring(0, 1); } return sembol.ToUpper(); } }
        #endregion "Method + Properties"
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            String theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                       + "@"
                       + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

            RuleFor(x => x.Firstname).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Firstname")) //Ad Validate
                .MinimumLength(3).WithMessage(LanguageHelper.GetString("User.Validate.Firstname2"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Email"));
                //.Matches(theEmailPattern);

            RuleFor(x => x.Username).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Username")); //username validate



        }
    }
}