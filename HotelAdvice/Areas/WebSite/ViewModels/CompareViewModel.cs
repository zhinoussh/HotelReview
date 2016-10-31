using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class CompareViewModel
    {
        public int HotelId { get; set; }

        public string HotelName { get; set; }

        public float avg_total_rating { get; set; }

        public float avg_Cleanliness_rating { get; set; }

        public float avg_Location_rating { get; set; }

        public float avg_Value_for_money_rating { get; set; }
    }
}