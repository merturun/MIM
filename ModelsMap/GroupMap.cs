using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()            
        {
            this.HasKey(e => e.GroupID);

            this.HasMany(e => e.Users)
                .WithMany(e => e.Groups)
                .Map(m =>
                {
                    m.MapLeftKey("GroupID");
                    m.MapRightKey("UserID");
                    m.ToTable("GroupUsers");
                });
        }
    }
}