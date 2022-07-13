using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()            
        {
            this.HasKey(e => e.ModuleID);

            this.HasMany(e => e.Licenses)
                .WithMany(e => e.Modules)
                .Map(m =>
                {
                    m.MapLeftKey("ModuleID");
                    m.MapRightKey("LicenseID");
                    m.ToTable("ModuleLicenses");
                });
        }
    }
}