using HotelAdvice.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HotelDetailAccordionViewModel
    {

        public string CityName { get; set; }

        public string CityDescription { get; set; }

        public string HotelAddress { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
        public string Description { get; set; }

        public string checkin { get; set; }

        public string checkout { get; set; }

        public double? distance_citycenter { get; set; }

        public double? distance_airport { get; set; }

        public List<String> restaurants { get; set; }

        public List<String> rooms { get; set; }

        public List<HotelAmenityViewModel> amenities { get; set; }
        public List<String> sightseeing { get; set; }
    }
}