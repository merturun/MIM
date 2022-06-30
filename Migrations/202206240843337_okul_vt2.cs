namespace Code_first.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class okul_vt2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogrenciler", "ana_adi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ogrenciler", "ana_adi");
        }
    }
}
