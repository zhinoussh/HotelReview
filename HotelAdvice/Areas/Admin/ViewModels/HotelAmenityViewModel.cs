using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.Admin.ViewModels
{
    public class HotelAmenityViewModel
    {
        public int AmenityID { get; set; }

        public String AmenityName { get; set; }

        public bool hotel_selected { get; set; }
    }
}