using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HotelAdvice.App_Code;
using PagedList;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using Microsoft.AspNet.Identity;
using HotelAdvice.Helper;
using System;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Controllers;


namespace HotelAdvice.Areas.WebSite.Controllers
{
    public class SearchHotelController : BaseController
    {
        public SearchHotelController(IServiceLayer service)
            : base(service)
        {

        }

        [HttpGet]
        public ActionResult ShowSearchResult(string destination_name, bool? citySearch, string HotelName, int? cityId, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5,string amenity)
        {
            SearchPageViewModel vm = DataService.Get_SearchResults(User.Identity.GetUserId(),destination_name
                , citySearch, HotelName, cityId, center, airport, score, Star1, Star2, Star3, Star4, Star5, amenity);
            return View(vm);
        }

        [HttpGet]
        [ActionName("HotelResults")]
        public PartialViewResult ShowSearchResult(int? page, string sort,string destination_name, bool? citySearch, int? cityId, string HotelName, int? center, int? airport, string score
                                                , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string amenity)
        {
            IPagedList page_list_hotels=DataService.Get_PartialHotelResults(User.Identity.GetUserId(),destination_name,citySearch,cityId, page, sort, HotelName, center, airport, score, Star1, Star2, Star3, Star4, Star5,amenity);
         
            return PartialView("_PartialHotelListResults", page_list_hotels);
        }

        [HttpGet]
        public ActionResult Advanced_Search(AdvancedSearchViewModel search_vm)
        {
            return Json(new { searchcriteria = search_vm},JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GeneralSearch(string destination_name)
        {
            return Json(new { destination_name = destination_name }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HotelDetails(int id)
        {
            HotelDetailViewModel vm=DataService.Get_HotelDetails(User.Identity.GetUserId(),id);

            return View(vm);
        }

    }
}