using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.ViewModels
{
    public class AmenityViewModel
    {
        public int CurrentPage { get; set; }
        public string CurrentFilter { get; set; }
       
        public int RowNum { get; set; }
        public int AmenityID { get; set; }

        [StringLength(50)]
        public String AmenityName { get; set; }

        public bool hotel_selected { get; set; }

    }
}