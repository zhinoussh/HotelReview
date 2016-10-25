namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_rating : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.tbl_rating",
            //    c => new
            //        {
            //            ratingId = c.Int(nullable: false, identity: true),
            //            rating = c.Int(nullable: false),
            //            UserId = c.String(maxLength: 128),
            //            HotelId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ratingId)
            //    .ForeignKey("dbo.tbl_Hotel", t => t.HotelId)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.HotelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_rating", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.tbl_rating", "HotelId", "dbo.tbl_Hotel");
            DropIndex("dbo.tbl_rating", new[] { "HotelId" });
            DropIndex("dbo.tbl_rating", new[] { "UserId" });
            DropTable("dbo.tbl_rating");
        }
    }
}
