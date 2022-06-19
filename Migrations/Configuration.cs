namespace MIM.Migrations
{
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
        }
    }
}
