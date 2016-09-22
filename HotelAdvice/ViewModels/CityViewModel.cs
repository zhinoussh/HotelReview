using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.ViewModels
{
    public class CityViewModel
    {
        public int cityID { get; set; }
        public int RowNum { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentFilter { get; set; }
       
        [StringLength(100, ErrorMessage = "max length has been exceeded.")]
        [Required(ErrorMessage = "This field is required.")]
        public string cityName { get; set; }

        [StringLength(500, ErrorMessage = "max length has been exceeded.")]
        public string cityAttractions { get; set; }
    }
}