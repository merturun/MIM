using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()            
        {
            this.HasKey(o => o.UserID);
            this.Property(o => o.Firstname).IsRequired();
            this.Property(o => o.Lastname).IsRequired();
            this.Property(o => o.Nickname);
            this.Property(o => o.Username).IsRequired();
            this.Property(o => o.Password).IsRequired();
            this.Property(o => o.Email).IsRequired();
            this.Property(o => o.BornDate);
            this.Property(o => o.IsActive);
            this.Property(o => o.SuperAdmin);
            this.Property(o => o.AvatarUrl);

            this.HasRequired(t => t.Title).WithMany().HasForeignKey(t => t.TitleID).WillCascadeOnDelete(false);
            this.HasRequired(t => t.Department).WithMany().HasForeignKey(t => t.DepartmentID).WillCascadeOnDelete(false);

            this.ToTable("Users");
        }
    }
}