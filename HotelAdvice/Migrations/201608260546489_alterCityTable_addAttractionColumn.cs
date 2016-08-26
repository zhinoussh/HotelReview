namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterCityTable_addAttractionColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_City", "CityAttractions", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_City", "CityAttractions");
        }
    }
}
