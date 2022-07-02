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
        public static User current;
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime bornDate { get; set; }
        public bool superAdmin { get; set; }
        public virtual ICollection<Group> groups { get; set; }

        public string fullname { get { return firstname + " " + lastname; } }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            String theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                       + "@"
                       + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

            RuleFor(x => x.firstname).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Firstname")) //Ad Validate
                .MinimumLength(3).WithMessage(LanguageHelper.GetString("User.Validate.Firstname2"));

            RuleFor(x => x.email).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Email"));
                //.Matches(theEmailPattern);

            RuleFor(x => x.username).NotEmpty().WithMessage(LanguageHelper.GetString("User.Validate.Username")); //username validate



        }
    }
}