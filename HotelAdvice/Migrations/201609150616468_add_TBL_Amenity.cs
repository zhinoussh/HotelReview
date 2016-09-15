namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TBL_Amenity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_amenity",
                c => new
                    {
                        AmenityID = c.Int(nullable: false, identity: true),
                        AmenityName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AmenityID);
            
            CreateTable(
                "dbo.tbl_hotel_amenity",
                c => new
                    {
                        HotelAmenityID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(),
                        AmenityID = c.Int(),
                    })
                .PrimaryKey(t => t.HotelAmenityID)
                .ForeignKey("dbo.tbl_Hotel", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_amenity", t => t.AmenityID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.AmenityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_hotel_amenity", "AmenityID", "dbo.tbl_amenity");
            DropForeignKey("dbo.tbl_hotel_amenity", "HotelID", "dbo.tbl_Hotel");
            DropIndex("dbo.tbl_hotel_amenity", new[] { "AmenityID" });
            DropIndex("dbo.tbl_hotel_amenity", new[] { "HotelID" });
            DropTable("dbo.tbl_hotel_amenity");
            DropTable("dbo.tbl_amenity");
        }
    }
}
