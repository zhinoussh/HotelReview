using HotelAdvice.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HotelDetailViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }

        [Display(Name="Rate by Users")]
        public float GuestRating { get; set; }
        public int YourRating { get; set; }

        public int review_num { get; set; }

        public bool is_favorite { get; set; }

        public int HotelStars { get; set; }

        public string CityName { get; set; }

        public HotelDetailAccordionViewModel accordion_detail { get; set; }
     
        public HotelPhotoAlbumViewModel photos { get; set; }

    }
}