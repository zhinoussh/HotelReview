using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HotelAdvice.App_Code;
using PagedList;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using Microsoft.AspNet.Identity;
using HotelAdvice.Helper;


namespace HotelAdvice.Areas.WebSite.Controllers
{
    public class SearchHotelController : Controller
    {
        DAL db = new DAL();
        const int defaultPageSize = 3;

        [HttpGet]
        public ActionResult ShowSearchResult(int city_id)
        {
            SearchPageViewModel vm = new SearchPageViewModel();

            vm.Advnaced_Search = Set_Advanced_Search();
           
            List<string> city_prop = db.get_city_byId(city_id);
            if (city_prop != null)
                vm.city_name = "Hotels in " + city_prop[0];
            
            //get hotel list in this city_id
            List<HotelSearchViewModel> lst_hotels = db.Search_Hotels_in_city(city_id,User.Identity.GetUserId());

            vm.paged_list_hotels = lst_hotels.ToPagedList(1, defaultPageSize);
           
            return View(vm);
        }

        [HttpGet]
        [ActionName("HotelResults")]
        public PartialViewResult ShowSearchResult(int city_id,int ?page,string sort)
        {
            return PartialView("_PartialHotelListResults", SetPartialHotelResult(city_id, page, sort));
        }

        private IPagedList<HotelSearchViewModel> SetPartialHotelResult(int city_id, int? page, string sort)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            //get hotel list in this city_id
            List<HotelSearchViewModel> lst_hotels = db.Search_Hotels_in_city(city_id, User.Identity.GetUserId());

            //sort
            switch (sort)
            {
                case "distance":
                    lst_hotels = lst_hotels.OrderBy(x => x.distance_citycenter).ToList();
                    break;

                case "rating":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.GuestRating).ToList();
                    break;
                case "5to1":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.HotelStars).ToList();
                    break;
                case "1to5":
                    lst_hotels = lst_hotels.OrderBy(x => x.HotelStars).ToList();
                    break;
                default:
                    break;

            }

            //pagination

            return  lst_hotels.ToPagedList(currentPageIndex, defaultPageSize);
        }

        [HttpGet]
        public ActionResult Advanced_Search(AdvancedSearchViewModel search_vm, string slider_guest_review)
        {
            SearchPageViewModel result_vm = new SearchPageViewModel();
            //this is advanced search
            //result_vm.city_search = "Matching results for your search....";

            //search_vm.Guest_Rating = slider_guest_review;
            //search_vm.City_List = new SelectList(db.get_cities().OrderBy(x => x.cityName).ToList(), "cityID", "cityName");
           
            //result_vm.Advnaced_Search = search_vm;
           
            return View("ShowSearchResult", result_vm);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavorite(int hotel_id, int city_id, int? page, string sort)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }
            else
            {
               
                int result = db.add_favorite_hotel(hotel_id, User.Identity.GetUserId());

                string partialview = RenderPartial.RenderViewToString(this.ControllerContext
                    , "~/Areas/WebSite/views/SearchHotel/_PartialHotelListResults.cshtml"
                    , SetPartialHotelResult(city_id, page, sort));

                if (result == 1)
                    return Json(new {  msg = "add_favorite_success", partial = partialview });
                else
                    return Json(new { msg = "favorite_already_exist", partial = partialview });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateHotel(int hotel_id, int your_rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });

            }
            return Json(new { msg = "add_success" });
        }

        public ActionResult HotelDetails(int id)
        {
            HotelDetailViewModel vm=db.get_hoteldetails(id);

            return View(vm);
        }
        private AdvancedSearchViewModel Set_Advanced_Search()
        {
            AdvancedSearchViewModel vm_search = new AdvancedSearchViewModel();
            vm_search.City_List = new SelectList(db.get_cities().OrderBy(x => x.cityName).ToList(), "cityID", "cityName");
            vm_search.selected_city = 0;

            List<KeyValuePair<int, string>> lst_locations = new List<KeyValuePair<int, string>>();
            lst_locations.Add(new KeyValuePair<int, string>(0, "Any Kilometer"));
            lst_locations.Add(new KeyValuePair<int, string>(1, "less than 1 km"));
            lst_locations.Add(new KeyValuePair<int, string>(2, "less than 2 km"));
            lst_locations.Add(new KeyValuePair<int, string>(3, "less than 3 km"));
            lst_locations.Add(new KeyValuePair<int, string>(4, "less than 4 km"));
            lst_locations.Add(new KeyValuePair<int, string>(5, "less than 5 km"));
            lst_locations.Add(new KeyValuePair<int, string>(10, "less than 10 km"));
            lst_locations.Add(new KeyValuePair<int, string>(20, "less than 20 km"));
            vm_search.Location = new SelectList(lst_locations, "Key", "Value");
            vm_search.distance_city_center = 0;
            vm_search.distance_airport = 0;

            vm_search.lst_amenity = db.get_Amenities();

            return vm_search;
        }

    }
}