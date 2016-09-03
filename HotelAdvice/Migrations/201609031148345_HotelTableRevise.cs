namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HotelTableRevise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Hotel", "Email", c => c.String());
            AddColumn("dbo.tbl_Hotel", "Website", c => c.String());
            AddColumn("dbo.tbl_Hotel", "Description", c => c.String());
            AddColumn("dbo.tbl_Hotel", "checkin", c => c.String());
            AddColumn("dbo.tbl_Hotel", "checkout", c => c.String());
            AddColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.String());
            AddColumn("dbo.tbl_Hotel", "distance_airport", c => c.String());
            DropColumn("dbo.tbl_Hotel", "Foodtype");
            DropColumn("dbo.tbl_Hotel", "wifi");
            DropColumn("dbo.tbl_Hotel", "parking");
            DropColumn("dbo.tbl_Hotel", "sport");
            DropColumn("dbo.tbl_Hotel", "OtherFacilities");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Hotel", "OtherFacilities", c => c.String());
            AddColumn("dbo.tbl_Hotel", "sport", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Hotel", "parking", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Hotel", "wifi", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Hotel", "Foodtype", c => c.String());
            DropColumn("dbo.tbl_Hotel", "distance_airport");
            DropColumn("dbo.tbl_Hotel", "distance_citycenter");
            DropColumn("dbo.tbl_Hotel", "checkout");
            DropColumn("dbo.tbl_Hotel", "checkin");
            DropColumn("dbo.tbl_Hotel", "Description");
            DropColumn("dbo.tbl_Hotel", "Website");
            DropColumn("dbo.tbl_Hotel", "Email");
        }
    }
}
