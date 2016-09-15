namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TBL_Sightseeing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", "dbo.tbl_room_type");
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "tbl_room_type_RoomTypeID" });
            CreateTable(
                "dbo.tbl_hotel_sightseeing",
                c => new
                    {
                        HotelSightseeingID = c.Int(nullable: false, identity: true),
                        SightseeingID = c.Int(),
                        HotelID = c.Int(),
                    })
                .PrimaryKey(t => t.HotelSightseeingID)
                .ForeignKey("dbo.tbl_sightseeing", t => t.SightseeingID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Hotel", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.SightseeingID)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.tbl_sightseeing",
                c => new
                    {
                        SightseeingID = c.Int(nullable: false, identity: true),
                        Sightseeing_Type = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SightseeingID);
            
            DropColumn("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", c => c.Int());
            DropForeignKey("dbo.tbl_hotel_sightseeing", "HotelID", "dbo.tbl_Hotel");
            DropForeignKey("dbo.tbl_hotel_sightseeing", "SightseeingID", "dbo.tbl_sightseeing");
            DropIndex("dbo.tbl_hotel_sightseeing", new[] { "HotelID" });
            DropIndex("dbo.tbl_hotel_sightseeing", new[] { "SightseeingID" });
            DropTable("dbo.tbl_sightseeing");
            DropTable("dbo.tbl_hotel_sightseeing");
            CreateIndex("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID");
            AddForeignKey("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", "dbo.tbl_room_type", "RoomTypeID");
        }
    }
}
