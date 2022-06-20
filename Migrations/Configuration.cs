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
            AutomaticMigrationsEnabled = false;
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
                bornDate = DateTime.Now,
                isActive = true,
                superAdmin = true
            };
            context.Users.Add(user);

            base.Seed(context);
        }
    }
}
