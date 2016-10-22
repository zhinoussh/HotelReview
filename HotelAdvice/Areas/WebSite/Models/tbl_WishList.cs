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
    public class tbl_WishList
    {
        public tbl_WishList()
        {

        }

        [Key]
        public int wishListId { get; set; }

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