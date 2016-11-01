using HotelAdvice.Areas.Account.Models;
using HotelAdvice.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.Models
{
    public class tbl_rating
    {
        [Key]
        public int ratingId { get; set; }

        public int rating { get; set; }

         [StringLength(200)]
        public string title_review { get; set; }
        
        [StringLength(int.MaxValue)]
        public string pros_review { get; set; }

        [StringLength(int.MaxValue)]
        public string cons_review { get; set; }
        public string review_date { get; set; }

        public int Cleanliness_rating { get; set; }
        public int Comfort_rating { get; set; }
        public int Location_rating { get; set; }
        public int Facilities_rating { get; set; }
        public int Staff_rating { get; set; }
        public int Value_for_money_rating { get; set; }


        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        public virtual tbl_Hotel Hotel { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}