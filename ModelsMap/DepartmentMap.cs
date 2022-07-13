using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()            
        {
            this.HasKey(o => o.DepartmentID);

            this.HasOptional(t => t.User).WithMany().HasForeignKey(t => t.UserID).WillCascadeOnDelete(false);
        }
    }
}