namespace MIM.Migrations
{
    using MIM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MIM.Config;
    using MIM.Migrations.Seed;

    internal sealed class Configuration : DbMigrationsConfiguration<MIMDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MIMDBContext context)
        {
            context.Configuration.LazyLoadingEnabled = false;

            Organization org = new Organization() { 
                Name = "MIM", Description = "Proje Ödevi", ContactName = "Mert Ürün", ContactEmail = "urn.mert@gmail.com", 
                ContactPhone = "05305756626", Address = "Bursa", IsActive = true };
            context.Organizations.Add(org);

            Title title1 = new Title() { 
                Name = "Fullstack Developer", Description = "All in One", Organization = org };
            context.Titles.Add(title1);

            Title title2 = new Title() { 
                Name = "Backend Developer", Description = "Sunucu tarafını geliştiren kimse.", Organization = org };
            context.Titles.Add(title2);

            Title title3 = new Title() { 
                Name = "Frontend Developer", Description = "İstemci tarafını geliştiren kimse.", Organization = org };
            context.Titles.Add(title3);

            Department dep = new Department() { Name = "IT", Description = "Bilgi İşlem", Organization = org };
            context.Departments.Add(dep);

            User user1 = new User() { 
                Organization = org, Firstname = "Mert", Lastname = "Ürün", Nickname = "Dawn", Username = "merturun", Title = title1, Password = "test", 
                Email = "urn.mert@gmail.com", BornDate = new DateTime(1988, 9, 2), IsActive = true, SuperAdmin = true, Department = dep };
            context.Users.Add(user1);

            User user2 = new User() { 
                Organization = org, Firstname = "Mert Can", Lastname = "Yılmaz", Nickname = "IOTA", Username = "mertcan", Title = title2, Password = "test",
                Email = "mertcnylmz0698@gmail.com", BornDate = new DateTime(1998, 6, 14), IsActive = true, SuperAdmin = true, Department = dep };
            context.Users.Add(user2);

            User user3 = new User() {
                Organization = org, Firstname = "İsmail", Lastname = "Gülaç", Nickname = "VDemented", Username = "vdemented", Title = title3, Password = "test", 
                Email = "ismailgulac@gmail.com", BornDate = new DateTime(1992, 3, 24), IsActive = true, SuperAdmin = true, Department = dep };

            User user4 = new User() { 
                Organization = org, Firstname = "Neo", Lastname = "Zion", Nickname = "demo", Username = "demo", Title = title3, Password = "demo",
                Email = "demo@mim.com", BornDate = new DateTime(2000, 1, 1), IsActive = true, SuperAdmin = false, Department = dep };
            context.Users.Add(user4);

            User user5 = new User() {
                Organization = org, Firstname = "Trinity", Lastname = "Zion", Nickname = "admin", Username = "admin", Title = title3, Password = "admin",
                Email = "admin@mim.com", BornDate = new DateTime(2000, 1, 1), IsActive = true, SuperAdmin = true, Department = dep };
            context.Users.Add(user5);

            GrantSeed gs = new GrantSeed(context);
            gs.SeedData();

            base.Seed(context);
        }
    }
}
