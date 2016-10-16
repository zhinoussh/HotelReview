using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Areas.Admin.Models
{
    public class tbl_City
    {
        public tbl_City(){
            Hotels = new HashSet<tbl_Hotel>();
        }


        [Key]
        public int CityId { get; set; }

        [StringLength(100)]
        public string CityName { get; set; }

        [StringLength(500)]
        public string CityAttractions { get; set; }

        public virtual ICollection<tbl_Hotel> Hotels { get; set; }


    }
}