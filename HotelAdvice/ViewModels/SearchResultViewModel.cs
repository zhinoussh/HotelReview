using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.ViewModels
{
    public class SearchResultViewModel
    {
        public string Destination_Name { get; set; }
        public string city_search { get; set; }

       // public List<HotelAdvice.ViewModels.HotelSearchViewModel> lst_hotels { get; set; }
        public IPagedList<HotelAdvice.ViewModels.HotelSearchViewModel> paged_list_hotels { get; set; }

        public AdvancedSearchViewModel Advnaced_Search { get; set; }
    }
}