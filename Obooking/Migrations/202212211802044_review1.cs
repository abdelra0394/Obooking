namespace Obooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Review_ID = c.Int(nullable: false, identity: true),
                        Review_content = c.String(nullable: false),
                        bookid = c.Int(nullable: false),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Review_ID)
                .ForeignKey("dbo.Books", t => t.bookid, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userid, cascadeDelete: true)
                .Index(t => t.bookid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "userid", "dbo.Users");
            DropForeignKey("dbo.Reviews", "bookid", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "userid" });
            DropIndex("dbo.Reviews", new[] { "bookid" });
            DropTable("dbo.Reviews");
        }
    }
}
