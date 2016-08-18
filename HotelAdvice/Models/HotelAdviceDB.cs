using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HotelAdvice.Models;

namespace HotelAdvice.App_Code
{
    public class HotelAdviceDB : DbContext
    {
        public virtual DbSet<tbl_Hotel> tbl_Hotel { get; set; }
        public virtual DbSet<tbl_City> tbl_city { get; set; }

        public HotelAdviceDB()
            : base("name=HotelAdviceConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_City>()
               .HasMany(e => e.Hotels)
               .WithOptional(e => e.City)
               .HasForeignKey(e => e.CityId)
               .WillCascadeOnDelete();
        }
    }   
}