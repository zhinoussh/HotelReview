namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TBL_hotelPhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Hotel_Photo",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoID)
                .ForeignKey("dbo.tbl_Hotel", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Hotel_Photo", "HotelID", "dbo.tbl_Hotel");
            DropIndex("dbo.tbl_Hotel_Photo", new[] { "HotelID" });
            DropTable("dbo.tbl_Hotel_Photo");
        }
    }
}
