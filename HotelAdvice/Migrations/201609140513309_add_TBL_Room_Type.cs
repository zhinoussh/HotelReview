namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TBL_Room_Type : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Hotel_Rooms",
                c => new
                    {
                        HotelRoomID = c.Int(nullable: false, identity: true),
                        HotelID = c.Int(),
                        RoomTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.HotelRoomID)
                .ForeignKey("dbo.tbl_room_type", t => t.RoomTypeID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Hotel", t => t.HotelID, cascadeDelete: true)
                .Index(t => t.HotelID)
                .Index(t => t.RoomTypeID);
            
            CreateTable(
                "dbo.tbl_room_type",
                c => new
                    {
                        RoomTypeID = c.Int(nullable: false, identity: true),
                        Room_Type = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoomTypeID);
            
            AddColumn("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", c => c.Int());
            CreateIndex("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID");
            AddForeignKey("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", "dbo.tbl_room_type", "RoomTypeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Hotel_Rooms", "HotelID", "dbo.tbl_Hotel");
            DropForeignKey("dbo.tbl_Hotel_Rooms", "RoomTypeID", "dbo.tbl_room_type");
            DropForeignKey("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID", "dbo.tbl_room_type");
            DropIndex("dbo.tbl_Hotel_Rooms", new[] { "RoomTypeID" });
            DropIndex("dbo.tbl_Hotel_Rooms", new[] { "HotelID" });
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "tbl_room_type_RoomTypeID" });
            DropColumn("dbo.tbl_Hotel_Restaurants", "tbl_room_type_RoomTypeID");
            DropTable("dbo.tbl_room_type");
            DropTable("dbo.tbl_Hotel_Rooms");
        }
    }
}
