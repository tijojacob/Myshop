namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PendingMigrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderItems", "Quantity", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
        }
    }
}
