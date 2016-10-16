using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Areas.Admin.Models
{
    public class tbl_room_type
    {
        public tbl_room_type()
        {
            HotelRooms = new HashSet<tbl_Hotel_Rooms>();
        }
       
        [Key]
        public int RoomTypeID { get; set; }
        
        [StringLength(50)]
        public string Room_Type { get; set; }

        public virtual ICollection<tbl_Hotel_Rooms> HotelRooms { get; set; }
    }
}