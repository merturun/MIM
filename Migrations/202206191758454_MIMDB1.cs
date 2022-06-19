namespace MIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIMDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "License_Id", "dbo.Licenses");
            DropIndex("dbo.Modules", new[] { "License_Id" });
            CreateTable(
                "dbo.ModuleLicenses",
                c => new
                    {
                        Module_Id = c.Int(nullable: false),
                        License_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Module_Id, t.License_Id })
                .ForeignKey("dbo.Modules", t => t.Module_Id, cascadeDelete: true)
                .ForeignKey("dbo.Licenses", t => t.License_Id, cascadeDelete: true)
                .Index(t => t.Module_Id)
                .Index(t => t.License_Id);
            
            DropColumn("dbo.Modules", "License_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "License_Id", c => c.Int());
            DropForeignKey("dbo.ModuleLicenses", "License_Id", "dbo.Licenses");
            DropForeignKey("dbo.ModuleLicenses", "Module_Id", "dbo.Modules");
            DropIndex("dbo.ModuleLicenses", new[] { "License_Id" });
            DropIndex("dbo.ModuleLicenses", new[] { "Module_Id" });
            DropTable("dbo.ModuleLicenses");
            CreateIndex("dbo.Modules", "License_Id");
            AddForeignKey("dbo.Modules", "License_Id", "dbo.Licenses", "Id");
        }
    }
}
