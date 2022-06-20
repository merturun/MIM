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
                        Organization_organizationID = c.Int(),
                        Manager_userID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_organizationID)
                .ForeignKey("dbo.Users", t => t.Manager_userID)
                .Index(t => t.Organization_organizationID)
                .Index(t => t.Manager_userID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        organizationID = c.Int(nullable: false),
                        firstname = c.String(),
                        lastname = c.String(),
                        nickname = c.String(),
                        username = c.String(),
                        titleID = c.Int(nullable: false),
                        password = c.String(),
                        email = c.String(),
                        isActive = c.Boolean(nullable: false),
                        bornDate = c.DateTime(nullable: false),
                        superAdmin = c.Boolean(nullable: false),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.userID)
                .ForeignKey("dbo.Organizations", t => t.organizationID, cascadeDelete: true)
                .ForeignKey("dbo.Titles", t => t.titleID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.organizationID)
                .Index(t => t.titleID)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Organization_organizationID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_organizationID)
                .Index(t => t.Organization_organizationID);
            
            CreateTable(
                "dbo.Grants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        organizationID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        contactName = c.String(),
                        contactEmail = c.String(),
                        contactPhone = c.String(),
                        language = c.String(),
                        adres = c.String(),
                        isActive = c.Boolean(nullable: false),
                        parentID = c.Int(nullable: false),
                        parent_organizationID = c.Int(),
                    })
                .PrimaryKey(t => t.organizationID)
                .ForeignKey("dbo.Organizations", t => t.parent_organizationID)
                .Index(t => t.parent_organizationID);
            
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Organization_organizationID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_organizationID)
                .Index(t => t.Organization_organizationID);
            
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
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        titleID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.titleID);
            
            CreateTable(
                "dbo.GrantGroups",
                c => new
                    {
                        Grant_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Grant_Id, t.Group_Id })
                .ForeignKey("dbo.Grants", t => t.Grant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Grant_Id)
                .Index(t => t.Group_Id);
            
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
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        User_userID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.User_userID })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_userID, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.User_userID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Manager_userID", "dbo.Users");
            DropForeignKey("dbo.Users", "titleID", "dbo.Titles");
            DropForeignKey("dbo.GroupUsers", "User_userID", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Organization_organizationID", "dbo.Organizations");
            DropForeignKey("dbo.Users", "organizationID", "dbo.Organizations");
            DropForeignKey("dbo.Licenses", "Organization_organizationID", "dbo.Organizations");
            DropForeignKey("dbo.ModuleLicenses", "License_Id", "dbo.Licenses");
            DropForeignKey("dbo.ModuleLicenses", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Departments", "Organization_organizationID", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "parent_organizationID", "dbo.Organizations");
            DropForeignKey("dbo.GrantGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GrantGroups", "Grant_Id", "dbo.Grants");
            DropIndex("dbo.GroupUsers", new[] { "User_userID" });
            DropIndex("dbo.GroupUsers", new[] { "Group_Id" });
            DropIndex("dbo.ModuleLicenses", new[] { "License_Id" });
            DropIndex("dbo.ModuleLicenses", new[] { "Module_Id" });
            DropIndex("dbo.GrantGroups", new[] { "Group_Id" });
            DropIndex("dbo.GrantGroups", new[] { "Grant_Id" });
            DropIndex("dbo.Licenses", new[] { "Organization_organizationID" });
            DropIndex("dbo.Organizations", new[] { "parent_organizationID" });
            DropIndex("dbo.Groups", new[] { "Organization_organizationID" });
            DropIndex("dbo.Users", new[] { "Department_Id" });
            DropIndex("dbo.Users", new[] { "titleID" });
            DropIndex("dbo.Users", new[] { "organizationID" });
            DropIndex("dbo.Departments", new[] { "Manager_userID" });
            DropIndex("dbo.Departments", new[] { "Organization_organizationID" });
            DropTable("dbo.GroupUsers");
            DropTable("dbo.ModuleLicenses");
            DropTable("dbo.GrantGroups");
            DropTable("dbo.Titles");
            DropTable("dbo.Modules");
            DropTable("dbo.Licenses");
            DropTable("dbo.Organizations");
            DropTable("dbo.Grants");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
        }
    }
}
