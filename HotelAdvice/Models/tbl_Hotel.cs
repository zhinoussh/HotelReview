using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HotelAdvice.Models
{
    public class tbl_Hotel
    {
        public tbl_Hotel()
        {
            HotelRests = new HashSet<tbl_Hotel_Restaurants>();
            HotelRooms = new HashSet<tbl_Hotel_Rooms>();
        }

        [Key]
        public int HotelId { get; set; }
        
        [StringLength(100)]
        public string HotelName { get; set; }

        [StringLength(500)]
        public string HotelAddress { get; set; }
        
        [StringLength(20)]
        public string Tel { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(20)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Website { get; set; }
        public int HotelStars { get; set; }

        [StringLength(int.MaxValue)]
        public string Description { get; set; }

        [StringLength(20)]
        public string checkin { get; set; }
        [StringLength(20)]
        public string checkout { get; set; }
        [StringLength(20)]
        public string distance_citycenter { get; set; }
        [StringLength(20)]
        public string distance_airport { get; set; }

        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual tbl_City City { get; set; }

        public virtual ICollection<tbl_Hotel_Restaurants> HotelRests { get; set; }
        public virtual ICollection<tbl_Hotel_Rooms> HotelRooms { get; set; }

    }
}