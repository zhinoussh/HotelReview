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


namespace HotelAdvice.Areas.WebSite.Controllers
{
    public class SearchHotelController : Controller
    {
        
        const int defaultPageSize = 3;

        IDataRepository db;

        public SearchHotelController(IDataRepository repo)
        {
            db = repo;
        }

        [HttpGet]
        public ActionResult ShowSearchResult( bool? citySearch,string HotelName, int? cityId, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5)
        {
            SearchPageViewModel vm = new SearchPageViewModel();
            vm.Advnaced_Search = Set_Advanced_Search();

            if (citySearch.HasValue && citySearch.Value==true)
            {
                List<string> city_prop = db.get_city_byId(cityId.Value);
                if (city_prop != null)
                    vm.city_name = "Hotels in " + city_prop[0];

                //get hotel list in this city_id
                List<HotelSearchViewModel> lst_hotels = db.Search_Hotels_in_city(cityId.Value, User.Identity.GetUserId());

                vm.paged_list_hotels = lst_hotels.ToPagedList(1, defaultPageSize);
                return View(vm);
            }
            else
            {
                //set advanced search
                vm.Advnaced_Search.Hotel_Name = HotelName;
                vm.Advnaced_Search.selected_city = cityId.HasValue ? cityId.Value : 0;
                vm.Advnaced_Search.distance_airport = airport.HasValue?airport.Value:1000;
                vm.Advnaced_Search.distance_city_center = center.HasValue ? center.Value :1000;
                vm.Advnaced_Search.Guest_Rating = score;
                vm.Advnaced_Search.Star1 = Star1.Value;
                vm.Advnaced_Search.Star2 = Star2.Value;
                vm.Advnaced_Search.Star3 = Star3.Value;
                vm.Advnaced_Search.Star4 = Star4.Value;
                vm.Advnaced_Search.Star5 = Star5.Value;

                //this is advanced search
                vm.city_name = "Matching results for your search....";

                //set advanced search
                if (score.Length > 0)
                {
                    string[] temp = score.Split(new char[] { ',' });
                    vm.Advnaced_Search.Min_Guest_Rating = float.Parse(temp[0]);
                    vm.Advnaced_Search.Max_Guest_Rating = float.Parse(temp[1]);
                }
               
                string stars = "";
                if (Star1 == true)
                    stars += "1,";

                if (Star2 == true)
                    stars += "2,";

                if (Star3 == true)
                    stars += "3,";

                if (Star4 == true)
                    stars += "4,";

                if (Star5 == true)
                    stars += "5,";

                if (stars.Length > 0)
                    stars = stars.Remove(stars.Length - 1, 1);

                vm.Advnaced_Search.hotel_stars = stars;

                //get hotel list by these criterias
                List<HotelSearchViewModel> lst_hotels = db.Advanced_Search(vm.Advnaced_Search, User.Identity.GetUserId());

                vm.paged_list_hotels = lst_hotels.ToPagedList(1, defaultPageSize);

                return View(vm);
            }
        }

        [HttpGet]
        [ActionName("HotelResults")]
        public PartialViewResult ShowSearchResult(int? page, string sort, int? cityId, string HotelName, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5)
        {
            return PartialView("_PartialHotelListResults", SetPartialHotelResult(cityId, page, sort, HotelName, center, airport, score, Star1, Star2, Star3, Star4, Star5));
        }


        private IPagedList<HotelSearchViewModel> SetPartialHotelResult(int? cityId, int? page, string sort, string HotelName, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            //get hotel list in this city_id
            AdvancedSearchViewModel vm = new AdvancedSearchViewModel();
            vm.Hotel_Name = HotelName;
            vm.selected_city = cityId.HasValue ? cityId.Value : 0;
            vm.distance_airport = airport.HasValue ? airport.Value : 1000;
            vm.distance_city_center = center.HasValue ? center.Value : 1000;
            vm.Guest_Rating = score;
            vm.Star1 = Star1.HasValue ? Star1.Value : false;
            vm.Star2 = Star2.HasValue ? Star2.Value : false;
            vm.Star3 = Star3.HasValue ? Star3.Value : false;
            vm.Star4 = Star4.HasValue ? Star4.Value : false;
            vm.Star5 = Star5.HasValue ? Star5.Value : false; 


            if (!String.IsNullOrEmpty(score))
            {
                string[] temp = score.Split(new char[] { ',' });
                vm.Min_Guest_Rating = float.Parse(temp[0]);
                vm.Max_Guest_Rating = float.Parse(temp[1]);
            }
            else
            {
                vm.Min_Guest_Rating = 0;
                vm.Max_Guest_Rating = 5;
            }

            string stars = "";
            if (Star1 == true)
                stars += "1,";

            if (Star2 == true)
                stars += "2,";

            if (Star3 == true)
                stars += "3,";

            if (Star4 == true)
                stars += "4,";

            if (Star5 == true)
                stars += "5,";

            if (stars.Length > 0)
                stars = stars.Remove(stars.Length - 1, 1);

            vm.hotel_stars = stars;

            List<HotelSearchViewModel> lst_hotels = db.Advanced_Search(vm, User.Identity.GetUserId());

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
            search_vm.Guest_Rating = slider_guest_review;
            return Json(new { searchriteria = search_vm },JsonRequestBehavior.AllowGet);
        }

     
        public ActionResult HotelDetails(int id)
        {
            HotelDetailViewModel vm=db.get_hoteldetails(id,User.Identity.GetUserId());

            return View(vm);
        }
       
        private AdvancedSearchViewModel Set_Advanced_Search()
        {
            AdvancedSearchViewModel vm_search = new AdvancedSearchViewModel();

            List<CityViewModel> lst_city = db.get_cities().OrderBy(x => x.cityName).ToList();
            List<CityViewModel> search_city = lst_city;
            search_city.Add(new CityViewModel { cityID = 0, cityName = "Select City" });
            vm_search.City_List = new SelectList(search_city, "cityID", "cityName");
            vm_search.selected_city = 0; 

            List<KeyValuePair<int, string>> lst_locations = new List<KeyValuePair<int, string>>();
            lst_locations.Add(new KeyValuePair<int, string>(1000, "Any Kilometer"));
            lst_locations.Add(new KeyValuePair<int, string>(1, "less than 1 km"));
            lst_locations.Add(new KeyValuePair<int, string>(2, "less than 2 km"));
            lst_locations.Add(new KeyValuePair<int, string>(3, "less than 3 km"));
            lst_locations.Add(new KeyValuePair<int, string>(4, "less than 4 km"));
            lst_locations.Add(new KeyValuePair<int, string>(5, "less than 5 km"));
            lst_locations.Add(new KeyValuePair<int, string>(10, "less than 10 km"));
            lst_locations.Add(new KeyValuePair<int, string>(20, "less than 20 km"));
            vm_search.Location = new SelectList(lst_locations, "Key", "Value");
            vm_search.distance_city_center = 1000;
            vm_search.distance_airport = 1000;

            vm_search.lst_amenity = db.get_Amenities();

            return vm_search;
        }

    }
}