using HotelAdvice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.App_Code;

namespace HotelAdvice.Controllers
{
    public class SearchHotelController : Controller
    {
        DAL db = new DAL();

        [HttpGet]
        public ActionResult ShowSearchResult(SearchResultViewModel vm)
        {

            return View(vm);
        }

        [HttpGet]
        public ActionResult Advanced_Search(AdvancedSearchViewModel search_vm, string slider_guest_review)
        {
            SearchResultViewModel result_vm = new SearchResultViewModel();
            //this is advanced search
            result_vm.city_search = "Matching results for your search....";

            search_vm.Guest_Rating = slider_guest_review;
            search_vm.City_List = new SelectList(db.get_cities().OrderBy(x => x.cityName).ToList(), "cityID", "cityName");
           
            result_vm.Advnaced_Search = search_vm;
           
            return View("ShowSearchResult", result_vm);
        }

        [HttpGet]
        public ActionResult SearchHotels_byCity(int id)
        {
            SearchResultViewModel vm = new SearchResultViewModel();
            vm.Advnaced_Search = Set_Advanced_Search();
            List<string> city_prop = db.get_city_byId(id);
            if (city_prop != null)
                vm.city_search = "Hotels in " + city_prop[0];

            vm.lst_hotels = db.Search_Hotels_in_city(id);

             return View("ShowSearchResult", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavoite(int hote_id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });

            }
            return Json(new {msg="add_success"});
        }

        public ActionResult HotelDetails(int id)
        {

            return View();
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