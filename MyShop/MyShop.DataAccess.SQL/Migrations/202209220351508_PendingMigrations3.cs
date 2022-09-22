namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PendingMigrations3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetRoles", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.AspNetUsers", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.AspNetUserClaims", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.AspNetUserLogins", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.AspNetUserRoles", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.BasketItems", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Baskets", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Customers", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.OrderItems", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Orders", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.ProductCategories", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Products", "CreatedAt", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.ProductCategories", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Orders", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.OrderItems", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Customers", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Baskets", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.BasketItems", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.AspNetUserRoles", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.AspNetUserLogins", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.AspNetUserClaims", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.AspNetUsers", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.AspNetRoles", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
