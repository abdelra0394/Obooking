namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_bookid_in_review : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "bookid", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "bookid" });
            RenameColumn(table: "dbo.Reviews", name: "bookid", newName: "Book_BookId");
            AlterColumn("dbo.Reviews", "Book_BookId", c => c.Int());
            CreateIndex("dbo.Reviews", "Book_BookId");
            AddForeignKey("dbo.Reviews", "Book_BookId", "dbo.Books", "BookId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Book_BookId", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "Book_BookId" });
            AlterColumn("dbo.Reviews", "Book_BookId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reviews", name: "Book_BookId", newName: "bookid");
            CreateIndex("dbo.Reviews", "bookid");
            AddForeignKey("dbo.Reviews", "bookid", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
