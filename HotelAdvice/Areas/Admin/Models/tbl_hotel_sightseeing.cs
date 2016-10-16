using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.Admin.Models
{
    public class tbl_hotel_sightseeing
    {
        public tbl_hotel_sightseeing()
        {
                
        }

        [Key]
        public int HotelSightseeingID { get; set; }
        public int? SightseeingID { get; set; }
        public int? HotelID { get; set; }

        [ForeignKey("SightseeingID")]
        public virtual tbl_sightseeing sightseeing { get; set; }

        [ForeignKey("HotelID")]
        public virtual tbl_Hotel Hotel { get; set; }
    }
}