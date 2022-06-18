namespace MIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MIMDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Grants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Nickname = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        ActivationCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        BornDate = c.DateTime(nullable: false),
                        SuperAdmin = c.Boolean(nullable: false),
                        Organization_Id = c.Int(),
                        Title_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Titles", t => t.Title_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Title_Id);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Users", "Title_Id", "dbo.Titles");
            DropForeignKey("dbo.Users", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Licenses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "Parent_Id", "dbo.Organizations");
            DropForeignKey("dbo.Grants", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "Title_Id" });
            DropIndex("dbo.Users", new[] { "Organization_Id" });
            DropIndex("dbo.Licenses", new[] { "Organization_Id" });
            DropIndex("dbo.Organizations", new[] { "Parent_Id" });
            DropIndex("dbo.Grants", new[] { "Group_Id" });
            DropIndex("dbo.Groups", new[] { "Organization_Id" });
            DropTable("dbo.Modules");
            DropTable("dbo.Titles");
            DropTable("dbo.Users");
            DropTable("dbo.Licenses");
            DropTable("dbo.Organizations");
            DropTable("dbo.Grants");
            DropTable("dbo.Groups");
            DropTable("dbo.Departments");
        }
    }
}
