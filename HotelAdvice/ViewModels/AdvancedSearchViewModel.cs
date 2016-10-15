using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelAdvice.ViewModels
{
    public class AdvancedSearchViewModel
    {
        public AdvancedSearchViewModel()
        {

        }

        [Display(Name = "Hotel Name")]
        public string Hotel_Name { get; set; }

        [Display(Name = "Guest Rating")]
        public string Guest_Rating { get; set; }

        [Display(Name = "Amenities")]
        public List<AmenityViewModel> lst_amenity { get; set; }

        public SelectList Location { get; set; }

        [Display(Name = "from City Center")]
        public int distance_city_center { get; set; }

        [Display(Name = "from Airport")]
        public int distance_airport { get; set; }

        public SelectList City_List { get; set; }

        public int selected_city { get; set; }

        [Display(Name = "Star Rating")]
        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }
    }
}