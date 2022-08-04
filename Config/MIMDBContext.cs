using MIM.Models;
using MIM.ModelsMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MIM.Config
{
    public class MIMDBContext : DbContext
    {
        public MIMDBContext() : base(GetConnectionString())
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

        public static string GetConnectionString()
        {
            string mname = Environment.MachineName;
            switch (mname)
            {
                case "DESKTOP-9416S5E":
                    return "name=Mert";
                case "DESKTOP-8O0HBRJ":
                    return "name=Mertcan";
                case "DESKTOP-7DK0VR4":
                    return "name=Ismail";
                default:
                    break;
            }
            return "name=LOCAL";
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new TitleMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new GrantMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new LicenseMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}