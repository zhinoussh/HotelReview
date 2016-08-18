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
        public int HotelStars { get; set; }
        public string Foodtype { get; set; }
        public bool wifi { get; set; }
        public bool parking { get; set; }
        public bool sport { get; set; }

        [StringLength(int.MaxValue)]
        public string OtherFacilities { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual tbl_City City { get; set; }

    }
}