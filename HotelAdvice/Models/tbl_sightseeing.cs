using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Models
{
    public class tbl_sightseeing
    {
        public tbl_sightseeing()
        {
            Hotelsightseeing = new HashSet<tbl_hotel_sightseeing>();    
        }

        [Key]
        public int SightseeingID { get; set; }

        [StringLength(50)]
        public string Sightseeing_Type { get; set; }

        public virtual ICollection<tbl_hotel_sightseeing> Hotelsightseeing { get; set; }
    }
}