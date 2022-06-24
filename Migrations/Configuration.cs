namespace MIM.Migrations
{
    using MIM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MIM.Config;

    internal sealed class Configuration : DbMigrationsConfiguration<MIMDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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
                adres = "Bursa",
                isActive = true
            };
            context.Organizations.Add(org);

            Title title = new Title()
            {
                name = "Yazılım Geliştirici",
                description = "Bişiler yazan adam"
            };
            context.Titles.Add(title);

            User user = new User()
            {
                organization = org,
                firstname = "Mert",
                lastname = "Ürün",
                nickname = "Dawn",
                username = "merturun",
                title = title,
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
                title = title,
                password = "test",
                email = "???",
                bornDate = new DateTime(1998,6, 14),
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
                title = title,
                password = "test",
                email = "ismailgulac@gmail.com",
                bornDate = new DateTime(1992, 3, 24),
                isActive = true,
                superAdmin = true
            };
            context.Users.Add(user);

            base.Seed(context);
        }
    }
}
