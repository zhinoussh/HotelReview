namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelRestaurantsRelationsAndTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Hotel_Restaurants",
                c => new
                    {
                        HotelRestID = c.Int(nullable: false, identity: true),
                        RestaurantID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HotelRestID)
                .ForeignKey("dbo.tbl_Hotel", t => t.HotelID, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Restuarant", t => t.RestaurantID, cascadeDelete: true)
                .Index(t => t.RestaurantID)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.tbl_Restuarant",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Hotel_Restaurants", "RestaurantID", "dbo.tbl_Restuarant");
            DropForeignKey("dbo.tbl_Hotel_Restaurants", "HotelID", "dbo.tbl_Hotel");
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "HotelID" });
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "RestaurantID" });
            DropTable("dbo.tbl_Restuarant");
            DropTable("dbo.tbl_Hotel_Restaurants");
        }
    }
}
