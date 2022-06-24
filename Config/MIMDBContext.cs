using MIM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MIM.Config
{
public class MIMDBContext : DbContext
{
    public MIMDBContext() : base(@"Data Source=DESKTOP-9416S5E\SQLEXPRESS;Initial Catalog=MIMDB;Integrated Security=true")
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<License> Licenses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<MIM.Models.Grant> Grants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


    }
}