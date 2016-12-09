using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HomeViewModel
    {
        public IPagedList<CityViewModel> lst_city { get; set; }

        public AdvancedSearchViewModel Advanced_Search { get; set; }
    }
}