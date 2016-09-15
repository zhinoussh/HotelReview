using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdvice.Models
{
    public class tbl_Hotel_Rooms
    {
        public tbl_Hotel_Rooms() { }

        [Key]
        public int HotelRoomID { get; set; }

        public int? HotelID { get; set; }
        public int? RoomTypeID { get; set; }

        [ForeignKey("HotelID")]
        public virtual tbl_Hotel Hotel{ get; set; }

        [ForeignKey("RoomTypeID")]
        public virtual tbl_room_type RoomType { get; set; }


    }
}