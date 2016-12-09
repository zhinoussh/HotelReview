using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HotelSearchViewModel
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }
        public string CityName { get; set; }

        public int HotelStars { get; set; }
        public string Website { get; set; }

        public string Description { get; set; }

        public float GuestRating { get; set; }
        public int YourRating { get; set; }

        public double? distance_citycenter { get; set; }
        public int num_reviews { get; set; }
      
        public bool is_favorite { get; set; }

    }
}