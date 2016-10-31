using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HotelReviewViewModel
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public int HotelStars { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int hotel_count_city { get; set; }

        public string Email { get; set; }
        public string Tel { get; set; }

        public float GuestRating { get; set; }
        public int num_reviews { get; set; }
        public int rank_hotel { get; set; }
    }
}