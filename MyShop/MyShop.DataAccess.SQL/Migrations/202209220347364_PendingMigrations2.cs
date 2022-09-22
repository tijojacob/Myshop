namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PendingMigrations2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean());
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
        }
    }
}
