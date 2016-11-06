using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class DeleteReviewViewModel
    {
        public int hotelId { get; set; }
        public int currentPageIndex { get; set; }
        public string HotelName { get; set; }
        public string UserId { get; set; }
    }
}