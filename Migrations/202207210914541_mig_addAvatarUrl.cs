namespace MIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addAvatarUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarUrl");
        }
    }
}
