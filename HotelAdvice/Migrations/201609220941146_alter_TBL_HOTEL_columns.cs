namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_TBL_HOTEL_columns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Hotel", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.tbl_Hotel", "Website", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Hotel", "Website", c => c.String(maxLength: 20));
            AlterColumn("dbo.tbl_Hotel", "Email", c => c.String(maxLength: 20));
        }
    }
}
