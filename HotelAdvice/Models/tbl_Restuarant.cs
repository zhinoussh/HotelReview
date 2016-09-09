using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Models
{
    public class tbl_Restuarant
    {
        public tbl_Restuarant() {
            HotelRests = new HashSet<tbl_Hotel_Restaurants>();
        }
       
        [Key]
        public int RestaurantID { get; set; }
        [StringLength(50)]
        public string RestaurantName { get; set; }

        public virtual ICollection<tbl_Hotel_Restaurants> HotelRests { get; set; }
    }
}