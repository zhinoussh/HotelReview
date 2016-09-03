using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdvice.Models
{
    public class tbl_Hotel_Restaurants
    {
        tbl_Hotel_Restaurants() { }
      
        [Key]
        public int HotelRestID { get; set; }
        public int? RestaurantID { get; set; }
        public int? HotelID { get; set; }

        [ForeignKey("RestaurantID")]
        public virtual tbl_Restuarant Restaurant { get; set; }

        [ForeignKey("HotelID")]
        public virtual tbl_Hotel Hotel { get; set; }

    }
}