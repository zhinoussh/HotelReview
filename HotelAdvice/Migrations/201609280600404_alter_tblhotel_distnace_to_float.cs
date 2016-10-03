namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tblhotel_distnace_to_float : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.Single(nullable: true));
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.Single(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Hotel", "distance_airport", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "distance_citycenter", c => c.String(maxLength: 20));
        }
    }
}
