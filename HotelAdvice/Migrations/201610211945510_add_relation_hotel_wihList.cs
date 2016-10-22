namespace HotelAdvice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_relation_hotel_wihList : DbMigration
    {
        public override void Up()
        {
          //  DropForeignKey("dbo.tbl_WishList", "UserId", "dbo.ApplicationUsers");
           // AddForeignKey("dbo.tbl_WishList", "UserId", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_WishList", "UserId", "dbo.ApplicationUsers");
            AddForeignKey("dbo.tbl_WishList", "UserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
    }
}
