namespace Tiplr.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        InventoryDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Finalized = c.Boolean(nullable: false),
                        CreatedByUser = c.Guid(nullable: false),
                        LastModifiedDtTm = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdtUser = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId);
            
            CreateTable(
                "dbo.InventoryItem",
                c => new
                    {
                        InventoryItemId = c.Int(nullable: false, identity: true),
                        InventoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OnHandCount = c.Double(nullable: false),
                        LastModifiedDtTm = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdtUser = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Inventory", t => t.InventoryId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.InventoryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        ProductDescription = c.String(maxLength: 150),
                        CategoryId = c.Int(),
                        CountBy = c.String(nullable: false),
                        OrderBy = c.String(nullable: false),
                        UnitsPerPack = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Par = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedDtTm = c.DateTimeOffset(nullable: false, precision: 7),
                        LastModifiedDtTm = c.DateTimeOffset(nullable: false, precision: 7),
                        InactiveDtTm = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdtUser = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategory", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        InventoryItemId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OrderAmt = c.Int(nullable: false),
                        AmtReceived = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.InventoryItem", t => t.InventoryItemId)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.InventoryItemId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        InventoryId = c.Int(nullable: false),
                        OrderCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatusId = c.Int(nullable: false),
                        OrderDate = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedByUser = c.Guid(nullable: false),
                        FinalizeUser = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Inventory", t => t.InventoryId)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId)
                .Index(t => t.InventoryId)
                .Index(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        OrderStatusDisplay = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Order", "InventoryId", "dbo.Inventory");
            DropForeignKey("dbo.OrderItem", "InventoryItemId", "dbo.InventoryItem");
            DropForeignKey("dbo.InventoryItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.ProductCategory");
            DropForeignKey("dbo.InventoryItem", "InventoryId", "dbo.Inventory");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Order", new[] { "OrderStatusId" });
            DropIndex("dbo.Order", new[] { "InventoryId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.OrderItem", new[] { "InventoryItemId" });
            DropIndex("dbo.OrderItem", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.InventoryItem", new[] { "ProductId" });
            DropIndex("dbo.InventoryItem", new[] { "InventoryId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Order");
            DropTable("dbo.OrderItem");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Product");
            DropTable("dbo.InventoryItem");
            DropTable("dbo.Inventory");
        }
    }
}
