using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdvice.Areas.Admin.Models
{
    public class tbl_hotel_amenity
    {
        public tbl_hotel_amenity()
        {

        }

       [Key]
        public int HotelAmenityID { get; set; }
        
        public int? HotelID { get; set; }
        
        public int? AmenityID { get; set; }

        [ForeignKey("HotelID")]
        public virtual tbl_Hotel Hotel { get; set; }

        [ForeignKey("AmenityID")]
        public virtual tbl_amenity Amenity { get; set; }
    }
}