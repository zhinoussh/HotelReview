using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HotelAdvice.Models;
using System.Web.Mvc;

namespace HotelAdvice.ViewModels
{
    public class HotelViewModel
    {
        public int HotelId { get; set; }

        [StringLength(100, ErrorMessage = "max length has been exceeded.")]
        [Required( ErrorMessage = "This field is required.")]
        public string HotelName { get; set; }

        [StringLength(500, ErrorMessage = "max length has been exceeded.")]
        public string HotelAddress { get; set; }

        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string Tel { get; set; }
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string Fax { get; set; }
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string Email { get; set; }
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string Website { get; set; }
        public int HotelStars { get; set; }

        [StringLength(int.MaxValue, ErrorMessage = "max length has been exceeded.")]
        public string Description { get; set; }

        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        public string checkin { get; set; }
        
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string checkout { get; set; }
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string distance_citycenter { get; set; }
        [StringLength(20, ErrorMessage = "max length has been exceeded.")]
        public string distance_airport { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public SelectList lst_city { get; set; }

        public List<tbl_Restuarant> restaurants{ get; set; }
    }
}