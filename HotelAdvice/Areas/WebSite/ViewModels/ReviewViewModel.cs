using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class ReviewViewModel
    {
        public int reviewID { get; set; }

        [Display(Name = "Review Title")]
        public string reviewTitle { get; set; }
       
        [Display(Name = "Positive Aspects")]
        public string reviewPros { get; set; }

        [Display(Name = "Negative Aspects")]
        public string reviewCons { get; set; }
        
        public string reviewDate { get; set; }

        [Display(Name = "Overal Rating")]
        public float total_rating { get; set; }

        [Display(Name = "Cleanliness")]
        public float Cleanliness_rating { get; set; }

        [Display(Name = "Comfort")]
        public float Comfort_rating { get; set; }

        [Display(Name = "Location")]
        public float Location_rating { get; set; }

        [Display(Name = "Amenities")]
        public float Facilities_rating { get; set; }

        [Display(Name = "Staff")]
        public float Staff_rating { get; set; }

        [Display(Name = "Value for Money")]
        public float Value_for_money_rating { get; set; }

    }
}