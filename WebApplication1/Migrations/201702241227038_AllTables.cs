namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManufacturerModels",
                c => new
                    {
                        ManufacturerModelID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ManufacturerModelID);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ProductModelID = c.Int(nullable: false, identity: true),
                        URLImage = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Color = c.String(),
                        Price = c.Double(nullable: false),
                        ManufacturerModelID = c.Int(nullable: false),
                        PurchaseModel_PurchaseModelID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductModelID)
                .ForeignKey("dbo.ManufacturerModels", t => t.ManufacturerModelID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseModels", t => t.PurchaseModel_PurchaseModelID)
                .Index(t => t.ManufacturerModelID)
                .Index(t => t.PurchaseModel_PurchaseModelID);
            
            CreateTable(
                "dbo.PurchaseModels",
                c => new
                    {
                        PurchaseModelID = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Guid(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        CreditNum = c.String(),
                        ValidityMonth = c.String(),
                        ValidityYear = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PurchaseModelID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductModels", "PurchaseModel_PurchaseModelID", "dbo.PurchaseModels");
            DropForeignKey("dbo.PurchaseModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductModels", "ManufacturerModelID", "dbo.ManufacturerModels");
            DropIndex("dbo.PurchaseModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProductModels", new[] { "PurchaseModel_PurchaseModelID" });
            DropIndex("dbo.ProductModels", new[] { "ManufacturerModelID" });
            DropTable("dbo.PurchaseModels");
            DropTable("dbo.ProductModels");
            DropTable("dbo.ManufacturerModels");
        }
    }
}
