using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class LicenseMap : EntityTypeConfiguration<License>
    {
        public LicenseMap()            
        {
            this.HasKey(e => e.LicenseID);

            this.HasMany(e => e.Modules)
                .WithMany(e => e.Licenses)
                .Map(m =>
                {
                    m.MapLeftKey("LicenseID");
                    m.MapRightKey("ModuleID");
                    m.ToTable("ModuleLicenses");
                });
        }
    }
}