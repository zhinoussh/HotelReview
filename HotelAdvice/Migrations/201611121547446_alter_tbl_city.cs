namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_city : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_City", "CityAttractions", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_City", "CityAttractions", c => c.String(maxLength: 500));
        }
    }
}
