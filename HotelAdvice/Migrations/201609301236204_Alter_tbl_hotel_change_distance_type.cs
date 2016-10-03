namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_tbl_hotel_change_distance_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.Double());
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.Single(nullable: false));
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.Single(nullable: false));
        }
    }
}
