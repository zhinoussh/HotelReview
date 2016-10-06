using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.ViewModels
{
    public class HomeViewModel
    {
        public string Destination_Name { get; set; }
        public List<CityViewModel> lst_city { get; set; }

        public AdvancedSearchViewModel Advanced_Search { get; set; }
    }
}