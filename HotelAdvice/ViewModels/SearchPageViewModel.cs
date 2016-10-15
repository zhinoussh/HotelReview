using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelAdvice.App_Code;

namespace HotelAdvice.ViewModels
{
    public class SearchPageViewModel
    {
        public string Destination_Name { get; set; }
        public int city_id { get; set; }

        public string city_name { get; set; }

        public IPagedList<HotelSearchViewModel> paged_list_hotels { get; set; }

        public AdvancedSearchViewModel Advnaced_Search { get; set; }

    }
}