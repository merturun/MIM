using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Organization
    {
        public static Organization current;
        public Organization()
        {
            this.users = new HashSet<User>();
            this.licenses = new HashSet<License>();
            this.departments = new HashSet<Department>();
            this.childs = new HashSet<Organization>();
        }
        [Key]
        public int organizationID { get; set; }
        public string name { get; set; } // Organizasyon Adı
        public string description { get; set; } // Organizasyon hakkında Açıklama
        public string contactName { get; set; } // Organizasyonda İletişim kurulacak kişi
        public string contactEmail { get; set; } // Organizasyonda İletişim maili
        public string contactPhone { get; set; } // Organizasyonda İletişim numarası
        public string language { get; set; } // Organizasyon varsayılan dil
        public string adres { get; set; } // Organizasyon Adresi
        public bool isActive { get; set; } // Organizasyonun Lisansı var mı yok mu ?
        public int parentID { get; set; } // Ana Organizasyon
        public virtual Organization parent { get; set; } // Ana Organizasyon
        public virtual ICollection<Organization> childs { get; set; } // Alt Organizasyonlar
        public virtual ICollection<User> users { get; set; } // Kullanıcılar
        public virtual ICollection<License> licenses { get; set; } // Lisanslar
        public virtual ICollection<Department> departments { get; set; } // Departmanlar
    }
}