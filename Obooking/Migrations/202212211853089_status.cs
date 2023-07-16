namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "status");
        }
    }
}
