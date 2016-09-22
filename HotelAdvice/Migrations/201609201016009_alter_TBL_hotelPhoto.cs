namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_TBL_hotelPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Hotel_Photo", "photo_name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Hotel_Photo", "photo_name");
        }
    }
}
