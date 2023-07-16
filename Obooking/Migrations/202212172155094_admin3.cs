namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class admin3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        ImageBook = c.String(),
                        BooKDescription = c.String(nullable: false),
                        BookPopularity = c.String(nullable: false),
                        BookPrice = c.Int(nullable: false),
                        BookCount = c.Int(nullable: false),
                        BookEdition = c.String(nullable: false),
                        BookStatus = c.String(nullable: false),
                        BookCategory = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.BookID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "BookID", "dbo.Books");
            DropIndex("dbo.OrderDetails", new[] { "BookID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
