namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixHetlRestaurantRelations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "RestaurantID" });
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "HotelID" });
            AlterColumn("dbo.tbl_Hotel_Restaurants", "RestaurantID", c => c.Int());
            AlterColumn("dbo.tbl_Hotel_Restaurants", "HotelID", c => c.Int());
            CreateIndex("dbo.tbl_Hotel_Restaurants", "RestaurantID");
            CreateIndex("dbo.tbl_Hotel_Restaurants", "HotelID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "HotelID" });
            DropIndex("dbo.tbl_Hotel_Restaurants", new[] { "RestaurantID" });
            AlterColumn("dbo.tbl_Hotel_Restaurants", "HotelID", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_Hotel_Restaurants", "RestaurantID", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_Hotel_Restaurants", "HotelID");
            CreateIndex("dbo.tbl_Hotel_Restaurants", "RestaurantID");
        }
    }
}
