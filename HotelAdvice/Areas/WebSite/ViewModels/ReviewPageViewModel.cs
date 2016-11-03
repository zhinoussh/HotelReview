using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class ReviewPageViewModel
    {
        public HotelReviewViewModel hotel_properties { get; set; }
        public ScoreViewModel hotel_scores { get; set; }
        public List<AddReviewViewModel> lst_reviews { get; set; }

        public AddReviewViewModel YourReview { get; set; }
        public List<CompareViewModel> lst_compare_hotels { get; set; }
    }
}