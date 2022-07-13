using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MIM.ModelsMap
{
    public class TitleMap : EntityTypeConfiguration<Title>
    {
        public TitleMap()            
        {
            this.HasKey(o => o.TitleID);
        }
    }
}