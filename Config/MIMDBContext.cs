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
                case "Ismailin Bilgisayarı":
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

            //modelBuilder.Entity<Group>() // Gruplar <> Kullanıcılar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Users)
            //    .WithMany(e => e.Groups)
            //    .Map(m => m.ToTable("GroupUsers"));

            //modelBuilder.Entity<Grant>() // Yetkiler <> Gruplar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Groups)
            //    .WithMany(e => e.Grants)
            //    .Map(m => m.ToTable("GrantGroups"));


            //modelBuilder.Entity<Department>() // Departmanlar <> Kullanıcılar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Users)
            //    .WithMany(e => e.Departments)
            //    .Map(m => m.ToTable("DepartmentUsers"));

            //modelBuilder.Entity<Organization>() // Organizasyon <> Departmanlar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Departments)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Organization>() // Organizasyon <> Lisanslar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Licenses)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Organization>() // Organizasyon <> Kullanıcılar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Users)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Organization>() // Organizasyon <> Ünvanlar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Titles)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Organization>() // Organizasyon <> Gruplar
            //    .HasKey(e => e.ID)
            //    .HasMany(e => e.Groups)
            //    .WithRequired(e => e.Organization)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<User>() // Kullanıcı <> Ünvan
            //    .HasKey(e => e.ID)
            //    .HasRequired(e => e.Title)
            //    .WithMany()
            //    .HasForeignKey(e => e.TitleID);

            //modelBuilder.Entity<User>() // Kullanıcı <> Organizasyon
            //    .HasKey(e => e.ID)
            //    .HasRequired(e => e.Organization)
            //    .WithMany()
            //    .HasForeignKey(e => e.OrganizationID);

            //modelBuilder.Entity<Title>() // Ünvan <> Organizasyon
            //    .HasKey(e => e.ID)
            //    .HasRequired(e => e.Organization)
            //    .WithMany()
            //    .HasForeignKey(e => e.OrganizationID);
        }
    }
}