using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class GrantMap : EntityTypeConfiguration<Grant>
    {
        public GrantMap()            
        {
            this.HasKey(e => e.GrantID);

            this.HasMany(e => e.Groups)
                .WithMany(e => e.Grants)
                .Map(m =>
                {
                    m.MapLeftKey("GrantID");
                    m.MapRightKey("GroupID");
                    m.ToTable("GrantGroups");
                });
        }
    }
}