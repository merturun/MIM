using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class OrganizationMap : EntityTypeConfiguration<Organization>
    {
        public OrganizationMap()            
        {
            this.HasKey(o => o.OrganizationID);
            this.Property(o => o.Name).IsRequired();
            this.Property(o => o.Description);
            this.Property(o => o.ContactName).IsRequired();
            this.Property(o => o.ContactEmail).IsRequired();
            this.Property(o => o.ContactPhone).IsRequired();
            this.Property(o => o.Address);

            this.HasMany(t => t.Titles).WithRequired(t => t.Organization).HasForeignKey(t => t.OrganizationID).WillCascadeOnDelete(true);
            this.HasMany(t => t.Licenses).WithRequired(t => t.Organization).HasForeignKey(t => t.OrganizationID).WillCascadeOnDelete(true);
            this.HasMany(t => t.Users).WithRequired(t => t.Organization).HasForeignKey(t => t.OrganizationID).WillCascadeOnDelete(true);
            this.HasMany(t => t.Groups).WithRequired(t => t.Organization).HasForeignKey(t => t.OrganizationID).WillCascadeOnDelete(true);
            this.HasMany(t => t.Departments).WithRequired(t => t.Organization).HasForeignKey(t => t.OrganizationID).WillCascadeOnDelete(true);

            this.ToTable("Organizations");
        }
    }
}