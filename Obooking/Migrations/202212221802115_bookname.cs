namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Book_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Book_Name");
        }
    }
}
