using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class ScoreViewModel
    {
        public float avg_total_rating { get; set; }

        [Display(Name = "Cleanliness")]
        public float avg_Cleanliness_rating { get; set; }

        [Display(Name = "Comfort")]
        public float avg_Comfort_rating { get; set; }

        [Display(Name = "Location")]
        public float avg_Location_rating { get; set; }

        [Display(Name = "Amenities")]
        public float avg_Facilities_rating { get; set; }

        [Display(Name = "Staff")]
        public float avg_Staff_rating { get; set; }

        [Display(Name = "Value for Money")]
        public float avg_Value_for_money_rating { get; set; }

    }
}