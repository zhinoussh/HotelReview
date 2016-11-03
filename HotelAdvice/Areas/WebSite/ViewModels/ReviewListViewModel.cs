using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class ReviewListViewModel
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }

        public int review_num { get; set; }
        public string reviewTitle { get; set; }

        public string reviewPros { get; set; }

        public string reviewCons { get; set; }

        public string reviewDate { get; set; }

        public int overal_rating { get; set; }
    }
}