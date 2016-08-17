using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace HotelAdvice.App_Code
{
    public class HotelAdviceDB : DbContext
    {
        public HotelAdviceDB()
            : base("name=HotelAdviceConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }   
}