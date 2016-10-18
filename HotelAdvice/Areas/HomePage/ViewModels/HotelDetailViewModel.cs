using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.HomePage.ViewModels
{
    public class HotelDetailViewModel
    {
        public string Destination_Name { get; set; }

        public int HotelId { get; set; }
        public string HotelName { get; set; }

        [Display(Name="Rate by Users")]
        public string GuestRating { get; set; }
        public int review_num { get; set; }

        public string HotelAddress { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
        public int HotelStars { get; set; }


        public string Description { get; set; }

        public string checkin { get; set; }

        public string checkout { get; set; }

        public double? distance_citycenter { get; set; }

        public double? distance_airport { get; set; }


        public string CityName { get; set; }

        public List<String> restaurants { get; set; }

        public List<String> rooms { get; set; }

        public List<String> amenities { get; set; }
        public List<String> sightseeing { get; set; }
        public List<String> photos { get; set; }

    }
}