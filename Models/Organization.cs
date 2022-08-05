using MIM.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Organization
    {
        public static Organization current { get { return Current(); } }
        public static Organization Current()
        {
            int organizationid = 0;
            if (HttpContext.Current.Session["current_organizationID"] != null)
                organizationid = ((int)HttpContext.Current.Session["current_organizationID"]);
            else
                return new Organization(true);

            MIMDBContext db = new MIMDBContext();
            return db.Organizations.Where(x => x.OrganizationID == organizationid).First();
        }
        public Organization(bool isDefault = true)
        {
            Name = Description = "Default";
            Address = ContactEmail = ContactName = ContactPhone = "Default";
        }

        public Organization()
        {
            Licenses = new List<License>();
            Users = new List<User>();
            Groups = new List<Group>();
            Departments = new List<Department>();
            Titles = new List<Title>();
        }

        public int OrganizationID { get; set; }
        public string Name { get; set; } // Organizasyon Adı
        public string Description { get; set; } // Organizasyon hakkında Açıklama
        public string ContactName { get; set; } // Organizasyonda İletişim kurulacak kişi
        public string ContactEmail { get; set; } // Organizasyonda İletişim maili
        public string ContactPhone { get; set; } // Organizasyonda İletişim numarası
        public string Address { get; set; } // Organizasyon Adresi
        public bool IsActive { get; set; } // Organizasyonun Lisansı var mı yok mu ?

        public List<License> Licenses { get; set; }
        public List<User> Users { get; set; }
        public List<Department> Departments { get; set; }
        public List<Title> Titles { get; set; }
        public List<Group> Groups { get; set; }
    }
}