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
            Organization org = new Organization()
            {
                name = "MIM",
                description = "Proje Ödevi",
                contactName = "Mert Ürün",
                contactEmail = "urn.mert@gmail.com",
                contactPhone = "05305756626",
                language = "tr",
                adress = "Bursa",
                isActive = true
            };
            context.Organizations.Add(org);

            Title title1 = new Title()
            {
                name = "Fullstack Developer",
                description = "All in One",
                organization = org
            };
            context.Titles.Add(title1);
            Title title2 = new Title()
            {
                name = "Backend Developer",
                description = "Sunucu tarafını geliştiren kimse.",
                organization = org
            };
            context.Titles.Add(title2);
            Title title3 = new Title()
            {
                name = "Frontend Developer",
                description = "İstemci tarafını geliştiren kimse.",
                organization = org
            };
            context.Titles.Add(title3);

            User user = new User()
            {
                organization = org,
                firstname = "Mert",
                lastname = "Ürün",
                nickname = "Dawn",
                username = "merturun",
                title = title1,
                password = "test",
                email = "urn.mert@gmail.com",
                bornDate = new DateTime(1988, 9, 2),
                isActive = true,
                superAdmin = true
            };
            context.Users.Add(user);

            user = new User()
            {
                organization = org,
                firstname = "Mert Can",
                lastname = "Yılmaz",
                nickname = "Bilemedim",
                username = "mertcan",
                title = title2,
                password = "test",
                email = "???",
                bornDate = new DateTime(1998, 6, 14),
                isActive = true,
                superAdmin = true
            };
            context.Users.Add(user);

            user = new User()
            {
                organization = org,
                firstname = "İsmail",
                lastname = "Gülaç",
                nickname = "VDemented",
                username = "vdemented",
                title = title3,
                password = "test",
                email = "ismailgulac@gmail.com",
                bornDate = new DateTime(1992, 3, 24),
                isActive = true,
                superAdmin = true
            };
            context.Users.Add(user);

            base.Seed(context);

            GrantSeed gs = new GrantSeed(context);
            gs.SeedData();
        }
    }
}
