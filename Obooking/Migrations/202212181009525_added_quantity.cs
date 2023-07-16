namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_quantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "quantity");
        }
    }
}
