using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Models
{
    public class tbl_amenity
    {
        public tbl_amenity()
        {
            amenities = new HashSet<tbl_hotel_amenity>();
        }

        [Key]
        public int AmenityID { get; set; }

        [StringLength(50)]
        public String AmenityName { get; set; }

        public virtual ICollection<tbl_hotel_amenity> amenities { get; set; }
    }
}