namespace MIM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserID = c.Int(),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ContactName = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Grants",
                c => new
                    {
                        GrantID = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Controller = c.String(),
                    })
                .PrimaryKey(t => t.GrantID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
                        TitleID = c.Int(nullable: true),
                        DepartmentID = c.Int(nullable: false),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Nickname = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        BornDate = c.DateTime(nullable: false),
                        SuperAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Titles", t => t.TitleID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.TitleID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        TitleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TitleID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        LicenseID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LicenseID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleID);
            
            CreateTable(
                "dbo.GrantGroups",
                c => new
                    {
                        GrantID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrantID, t.GroupID })
                .ForeignKey("dbo.Grants", t => t.GrantID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GrantID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID })
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ModuleLicenses",
                c => new
                    {
                        LicenseID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LicenseID, t.ModuleID })
                .ForeignKey("dbo.Licenses", t => t.LicenseID, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.LicenseID)
                .Index(t => t.ModuleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Titles", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Licenses", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.ModuleLicenses", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleLicenses", "LicenseID", "dbo.Licenses");
            DropForeignKey("dbo.Groups", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.GroupUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.Users", "TitleID", "dbo.Titles");
            DropForeignKey("dbo.Users", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.GrantGroups", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GrantGroups", "GrantID", "dbo.Grants");
            DropForeignKey("dbo.Departments", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.ModuleLicenses", new[] { "ModuleID" });
            DropIndex("dbo.ModuleLicenses", new[] { "LicenseID" });
            DropIndex("dbo.GroupUsers", new[] { "UserID" });
            DropIndex("dbo.GroupUsers", new[] { "GroupID" });
            DropIndex("dbo.GrantGroups", new[] { "GroupID" });
            DropIndex("dbo.GrantGroups", new[] { "GrantID" });
            DropIndex("dbo.Licenses", new[] { "OrganizationID" });
            DropIndex("dbo.Titles", new[] { "OrganizationID" });
            DropIndex("dbo.Users", new[] { "DepartmentID" });
            DropIndex("dbo.Users", new[] { "TitleID" });
            DropIndex("dbo.Users", new[] { "OrganizationID" });
            DropIndex("dbo.Groups", new[] { "OrganizationID" });
            DropIndex("dbo.Departments", new[] { "OrganizationID" });
            DropIndex("dbo.Departments", new[] { "UserID" });
            DropTable("dbo.ModuleLicenses");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.GrantGroups");
            DropTable("dbo.Modules");
            DropTable("dbo.Licenses");
            DropTable("dbo.Titles");
            DropTable("dbo.Users");
            DropTable("dbo.Grants");
            DropTable("dbo.Groups");
            DropTable("dbo.Organizations");
            DropTable("dbo.Departments");
        }
    }
}
