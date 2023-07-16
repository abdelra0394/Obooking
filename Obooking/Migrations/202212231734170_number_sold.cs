namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class number_sold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "number_sold", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "number_sold");
        }
    }
}
