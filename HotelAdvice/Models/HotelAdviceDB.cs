using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HotelAdvice.Models;

namespace HotelAdvice
{
    public class HotelAdviceDB : DbContext
    {
        public virtual DbSet<tbl_Hotel> tbl_Hotel { get; set; }
        public virtual DbSet<tbl_City> tbl_city { get; set; }
        public virtual DbSet<tbl_Restuarant> tbl_Restaurant { get; set; }
        public virtual DbSet<tbl_Hotel_Restaurants> tbl_Hotel_Restaurants { get; set; }
        public virtual DbSet<tbl_room_type> tbl_Room_Type { get; set; }
        public virtual DbSet<tbl_Hotel_Rooms> tbl_Hotel_Rooms { get; set; }
        public virtual DbSet<tbl_amenity> tbl_Amenity { get; set; }
        public virtual DbSet<tbl_hotel_amenity> tbl_Hotel_Amenities { get; set; }
        public virtual DbSet<tbl_sightseeing> tbl_Sightseeing { get; set; }
        public virtual DbSet<tbl_hotel_sightseeing> tbl_Hotel_Sightseeings { get; set; }
        public virtual DbSet<tbl_Hotel_Photo> tbl_Hotel_Photo { get; set; }


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

            modelBuilder.Entity<tbl_Hotel>()
               .HasMany(e => e.HotelRests)
               .WithOptional(e => e.Hotel)
               .HasForeignKey(e => e.HotelID)
               .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_Restuarant>()
              .HasMany(e => e.HotelRests)
              .WithOptional(e => e.Restaurant)
              .HasForeignKey(e => e.RestaurantID)
              .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_Hotel>()
              .HasMany(e => e.HotelRooms)
              .WithOptional(e => e.Hotel)
              .HasForeignKey(e => e.HotelID)
              .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_room_type>()
              .HasMany(e => e.HotelRooms)
              .WithOptional(e => e.RoomType)
              .HasForeignKey(e => e.RoomTypeID)
              .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_Hotel>()
             .HasMany(e => e.HotelAmenities)
             .WithOptional(e => e.Hotel)
             .HasForeignKey(e => e.HotelID)
             .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_amenity>()
              .HasMany(e => e.amenities)
              .WithOptional(e => e.Amenity)
              .HasForeignKey(e => e.AmenityID)
              .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_Hotel>()
             .HasMany(e => e.HotelSightSeeings)
             .WithOptional(e => e.Hotel)
             .HasForeignKey(e => e.HotelID)
             .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_sightseeing>()
              .HasMany(e => e.Hotelsightseeing)
              .WithOptional(e => e.sightseeing)
              .HasForeignKey(e => e.SightseeingID)
              .WillCascadeOnDelete();

            modelBuilder.Entity<tbl_Hotel>()
             .HasMany(e => e.HotelPhotos)
             .WithOptional(e => e.hotel)
             .HasForeignKey(e => e.HotelID)
             .WillCascadeOnDelete();
        }
    }
}