using MIM.Config;
using MIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIM.Migrations.Seed
{
    //public class GrantSeed
    //{
    //    public MIMDBContext db;

    //    public GrantSeed(MIMDBContext db)
    //    {
    //        this.db = db;
    //    }

    //    public void SeedData()
    //    {

    //    }
    //}

    public class GrantSeed
    {
        public MIMDBContext db;
        List<Grant> list = new List<Grant>();

        public GrantSeed(MIMDBContext db)
        {
            this.db = db;
        }

        public void SeedData()
        {
        }
    }
}