using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HotelAdvice.ViewModels
{
    public class HomeViewModel
    {
        public string Destination_Name { get; set; }
        public List<CityViewModel> lst_city { get; set; }
        
        //Advanced Search
        [Display(Name="Hotel Name")]
        public string Hotel_Name { get; set; }
        
        [Display(Name = "Guest Rating")]
        public float Guest_Rating { get; set; }

        public SelectList Location { get; set; }
        public string selected_location { get; set; }

        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }

    }
}