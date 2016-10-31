namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBContext : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.tbl_rating", "title_review", c => c.String(maxLength: 200));
            //AddColumn("dbo.tbl_rating", "pros_review", c => c.String());
            //AddColumn("dbo.tbl_rating", "cons_review", c => c.String());
            //AddColumn("dbo.tbl_rating", "Cleanliness_rating", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_rating", "Comfort_rating", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_rating", "Location_rating", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_rating", "Facilities_rating", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_rating", "Staff_rating", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_rating", "Value_for_money_rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_rating", "Value_for_money_rating");
            DropColumn("dbo.tbl_rating", "Staff_rating");
            DropColumn("dbo.tbl_rating", "Facilities_rating");
            DropColumn("dbo.tbl_rating", "Location_rating");
            DropColumn("dbo.tbl_rating", "Comfort_rating");
            DropColumn("dbo.tbl_rating", "Cleanliness_rating");
            DropColumn("dbo.tbl_rating", "cons_review");
            DropColumn("dbo.tbl_rating", "pros_review");
            DropColumn("dbo.tbl_rating", "title_review");
        }
    }
}
