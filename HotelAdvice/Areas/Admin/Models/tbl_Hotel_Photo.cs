using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdvice.Areas.Admin.Models
{
    public class tbl_Hotel_Photo
    {
        public tbl_Hotel_Photo()
        {
                
        }

        [Key]
        public int PhotoID { get; set; }

        public int? HotelID { get; set; }
        public string photo_name { get; set; }

        [ForeignKey("HotelID")]
        public virtual tbl_Hotel hotel { get; set; }

    }
}