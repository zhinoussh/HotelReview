using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class UserPageViewModel
    {
        public IPagedList<HotelSearchViewModel> lst_wishList { get; set; }
        public IPagedList<HotelSearchViewModel> lst_reviews { get; set; }
        public IPagedList<HotelSearchViewModel> lst_rating { get; set; }
    }
}