namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAspNetUsersOrders : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(),
            //            CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(nullable: false),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(),
            //            CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
            //            AspNetRoles_Id = c.String(maxLength: 128),
            //            AspNetUserClaims_Id = c.String(maxLength: 128),
            //            AspNetUserLogins_Id = c.String(maxLength: 128),
            //            AspNetUserRoles_Id = c.String(maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetRoles", t => t.AspNetRoles_Id)
            //    .ForeignKey("dbo.AspNetUserClaims", t => t.AspNetUserClaims_Id)
            //    .ForeignKey("dbo.AspNetUserLogins", t => t.AspNetUserLogins_Id)
            //    .ForeignKey("dbo.AspNetUserRoles", t => t.AspNetUserRoles_Id)
            //    .Index(t => t.AspNetRoles_Id)
            //    .Index(t => t.AspNetUserClaims_Id)
            //    .Index(t => t.AspNetUserLogins_Id)
            //    .Index(t => t.AspNetUserRoles_Id);
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //            CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            LoginProvider = c.String(),
            //            ProviderKey = c.String(),
            //            CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(),
            //            CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        SurName = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        OrderStatus = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            //DropForeignKey("dbo.AspNetUsers", "AspNetUserRoles_Id", "dbo.AspNetUserRoles");
            //DropForeignKey("dbo.AspNetUsers", "AspNetUserLogins_Id", "dbo.AspNetUserLogins");
            //DropForeignKey("dbo.AspNetUsers", "AspNetUserClaims_Id", "dbo.AspNetUserClaims");
            //DropForeignKey("dbo.AspNetUsers", "AspNetRoles_Id", "dbo.AspNetRoles");
            //DropIndex("dbo.OrderItems", new[] { "OrderId" });
            //DropIndex("dbo.AspNetUsers", new[] { "AspNetUserRoles_Id" });
            //DropIndex("dbo.AspNetUsers", new[] { "AspNetUserLogins_Id" });
            //DropIndex("dbo.AspNetUsers", new[] { "AspNetUserClaims_Id" });
            //DropIndex("dbo.AspNetUsers", new[] { "AspNetRoles_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.AspNetRoles");
        }
    }
}
