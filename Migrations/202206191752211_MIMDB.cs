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
                        Organization_Id = c.Int(),
                        Manager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Users", t => t.Manager_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Manager_Id);
            
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
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Titles", t => t.Title_Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Title_Id)
                .Index(t => t.Department_Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ContactName = c.String(),
                        ContactEmail = c.String(),
                        ContactPhone = c.String(),
                        language = c.String(),
                        Adres = c.String(),
                        isActive = c.Boolean(nullable: false),
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
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        License_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Licenses", t => t.License_Id)
                .Index(t => t.License_Id);
            
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
                "dbo.GroupUsers",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.User_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Manager_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Title_Id", "dbo.Titles");
            DropForeignKey("dbo.GroupUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Users", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Licenses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Modules", "License_Id", "dbo.Licenses");
            DropForeignKey("dbo.Departments", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "Parent_Id", "dbo.Organizations");
            DropForeignKey("dbo.GrantGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GrantGroups", "Grant_Id", "dbo.Grants");
            DropIndex("dbo.GroupUsers", new[] { "User_Id" });
            DropIndex("dbo.GroupUsers", new[] { "Group_Id" });
            DropIndex("dbo.GrantGroups", new[] { "Group_Id" });
            DropIndex("dbo.GrantGroups", new[] { "Grant_Id" });
            DropIndex("dbo.Modules", new[] { "License_Id" });
            DropIndex("dbo.Licenses", new[] { "Organization_Id" });
            DropIndex("dbo.Organizations", new[] { "Parent_Id" });
            DropIndex("dbo.Groups", new[] { "Organization_Id" });
            DropIndex("dbo.Users", new[] { "Department_Id" });
            DropIndex("dbo.Users", new[] { "Title_Id" });
            DropIndex("dbo.Users", new[] { "Organization_Id" });
            DropIndex("dbo.Departments", new[] { "Manager_Id" });
            DropIndex("dbo.Departments", new[] { "Organization_Id" });
            DropTable("dbo.GroupUsers");
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
