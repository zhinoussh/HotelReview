using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName="varchar(MAX)")]
        public string CityAttractions { get; set; }

        public virtual ICollection<tbl_Hotel> Hotels { get; set; }


    }
}