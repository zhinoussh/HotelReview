namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_TBL_HOTEL_AND_TBL_CITY : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.tbl_Hotel",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        HotelName = c.String(maxLength: 100),
                        HotelAddress = c.String(maxLength: 500),
                        Tel = c.String(maxLength: 20),
                        Fax = c.String(maxLength: 20),
                        HotelStars = c.Int(nullable: false),
                        Foodtype = c.String(),
                        wifi = c.Boolean(nullable: false),
                        parking = c.Boolean(nullable: false),
                        sport = c.Boolean(nullable: false),
                        OtherFacilities = c.String(),
                        CityId = c.Int(),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.tbl_City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Hotel", "CityId", "dbo.tbl_City");
            DropIndex("dbo.tbl_Hotel", new[] { "CityId" });
            DropTable("dbo.tbl_Hotel");
            DropTable("dbo.tbl_City");
        }
    }
}
