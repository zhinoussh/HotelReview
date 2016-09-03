namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Hotel", "Email", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "Website", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "checkin", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "checkout", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Restuarant", "RestaurantName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Restuarant", "RestaurantName", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "checkout", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "checkin", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "Website", c => c.String());
            AlterColumn("dbo.tbl_Hotel", "Email", c => c.String());
        }
    }
}
