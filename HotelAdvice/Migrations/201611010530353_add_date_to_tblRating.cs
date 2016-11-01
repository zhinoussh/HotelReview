namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_date_to_tblRating : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.tbl_rating", "review_date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_rating", "review_date");
        }
    }
}
