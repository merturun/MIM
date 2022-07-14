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
            db.Grants.Add(new Grant() { Action = "Create", Controller = "Users" });
            db.Grants.Add(new Grant() { Action = "Edit", Controller = "Users" });
            db.Grants.Add(new Grant() { Action = "Delete", Controller = "Users" });
            db.Grants.Add(new Grant() { Action = "Table", Controller = "Users" });
            db.Grants.Add(new Grant() { Action = "Show", Controller = "Users" });
            db.Grants.Add(new Grant() { Action = "All", Controller = "Users" });

            db.Grants.Add(new Grant() { Action = "Create", Controller = "Titles" });
            db.Grants.Add(new Grant() { Action = "Edit", Controller = "Titles" });
            db.Grants.Add(new Grant() { Action = "Delete", Controller = "Titles" });
            db.Grants.Add(new Grant() { Action = "Table", Controller = "Titles" });
            db.Grants.Add(new Grant() { Action = "Show", Controller = "Titles" });
            db.Grants.Add(new Grant() { Action = "All", Controller = "Titles" });

            db.Grants.Add(new Grant() { Action = "Create", Controller = "Departments" });
            db.Grants.Add(new Grant() { Action = "Edit", Controller = "Departments" });
            db.Grants.Add(new Grant() { Action = "Delete", Controller = "Departments" });
            db.Grants.Add(new Grant() { Action = "Table", Controller = "Departments" });
            db.Grants.Add(new Grant() { Action = "Show", Controller = "Departments" });
            db.Grants.Add(new Grant() { Action = "All", Controller = "Departments" });

            db.Grants.Add(new Grant() { Action = "Create", Controller = "Groups" });
            db.Grants.Add(new Grant() { Action = "Edit", Controller = "Groups" });
            db.Grants.Add(new Grant() { Action = "Delete", Controller = "Groups" });
            db.Grants.Add(new Grant() { Action = "Table", Controller = "Groups" });
            db.Grants.Add(new Grant() { Action = "Show", Controller = "Groups" });
            db.Grants.Add(new Grant() { Action = "All", Controller = "Groups" });
        }
    }
}