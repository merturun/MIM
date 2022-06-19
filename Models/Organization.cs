using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class Organization
    {
        public Organization()
        {
            this.Users = new HashSet<User>();
            this.Licenses = new HashSet<License>();
            this.Departments = new HashSet<Department>();
            this.Childs = new HashSet<Organization>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } // Organizasyon Adı
        public string Description { get; set; } // Organizasyon hakkında Açıklama
        public string ContactName { get; set; } // Organizasyonda İletişim kurulacak kişi
        public string ContactEmail { get; set; } // Organizasyonda İletişim maili
        public string ContactPhone { get; set; } // Organizasyonda İletişim numarası
        public string language { get; set; } // Organizasyon varsayılan dil
        public string Adres { get; set; } // Organizasyon Adresi
        public bool isActive { get; set; } // Organizasyonun Lisansı var mı yok mu ?
        public Organization Parent { get; set; } // Ana Organizasyon
        public virtual ICollection<Organization> Childs { get; set; } // Alt Organizasyonlar
        public virtual ICollection<User> Users { get; set; } // Kullanıcılar
        public virtual ICollection<License> Licenses { get; set; } // Lisanslar
        public virtual ICollection<Department> Departments { get; set; } // Lisanslar
    }
}