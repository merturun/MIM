namespace Code_first.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class okul_vt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dersler",
                c => new
                    {
                        Ders_no = c.Byte(nullable: false),
                        ders_adi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Ders_no);
            
            CreateTable(
                "dbo.Nots",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ogno = c.Int(nullable: false),
                        Ders_no = c.Byte(nullable: false),
                        yaz1 = c.Byte(nullable: false),
                        yaz2 = c.Byte(),
                        perf = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Ogrenciler", t => t.ogno, cascadeDelete: true)
                .ForeignKey("dbo.Dersler", t => t.Ders_no, cascadeDelete: true)
                .Index(t => t.ogno)
                .Index(t => t.Ders_no);
            
            CreateTable(
                "dbo.Ogrenciler",
                c => new
                    {
                        ogno = c.Int(nullable: false, identity: true),
                        ad_soyad = c.String(nullable: false, maxLength: 30),
                        tc_kimlik = c.Long(nullable: false),
                        dog_tar = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        adres = c.String(),
                    })
                .PrimaryKey(t => t.ogno)
                .Index(t => t.tc_kimlik, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nots", "Ders_no", "dbo.Dersler");
            DropForeignKey("dbo.Nots", "ogno", "dbo.Ogrenciler");
            DropIndex("dbo.Ogrenciler", new[] { "tc_kimlik" });
            DropIndex("dbo.Nots", new[] { "Ders_no" });
            DropIndex("dbo.Nots", new[] { "ogno" });
            DropTable("dbo.Ogrenciler");
            DropTable("dbo.Nots");
            DropTable("dbo.Dersler");
        }
    }
}
