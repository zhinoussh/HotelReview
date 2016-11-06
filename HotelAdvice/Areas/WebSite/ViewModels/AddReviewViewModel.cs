using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class AddReviewViewModel
    {
        public int RateId { get; set; }
        public int HotelId { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Review Title")]
        public string reviewTitle { get; set; }
       
        [Display(Name = "Positive Aspects")]
        public string reviewPros { get; set; }

        [Display(Name = "Negative Aspects")]
        public string reviewCons { get; set; }
        
        public string reviewDate { get; set; }

        [Display(Name = "Overal Rating")]
        public int total_rating { get; set; }

        [Display(Name = "Cleanliness")]
        public int Cleanliness_rating { get; set; }

        [Display(Name = "Comfort")]
        public int Comfort_rating { get; set; }

        [Display(Name = "Location")]
        public int Location_rating { get; set; }

        [Display(Name = "Amenities")]
        public int Facilities_rating { get; set; }

        [Display(Name = "Staff")]
        public int Staff_rating { get; set; }

        [Display(Name = "Value for Money")]
        public int Value_for_money_rating { get; set; }

        public bool fromProfilePage { get; set; }

        public int currentPageIndex { get; set; }

    }
}