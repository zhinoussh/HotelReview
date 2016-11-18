using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.Areas.Admin.ViewModels
{
    public class AmenityViewModel
    {
        public int CurrentPage { get; set; }
        public string CurrentFilter { get; set; }
       
        public int RowNum { get; set; }
        public int AmenityID { get; set; }

        [StringLength(50, ErrorMessage = "max length has been exceeded.")]
        [Required(ErrorMessage = "This field is required.")]
        public String AmenityName { get; set; }

        public bool hotel_selected { get; set; }

    }
}