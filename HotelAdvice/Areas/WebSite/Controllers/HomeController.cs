using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HotelAdvice.Areas.WebSite.ViewModels;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Controllers;

namespace HotelAdvice.Areas.WebSite.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IServiceLayer service)
            : base(service)
        {
        }

    
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HomeViewModel vm = new HomeViewModel();
            vm.lst_city = DataService.get_cities().OrderBy(x=>x.cityName).ToList();

           // List<CityViewModel> search_city=vm.lst_city;
           // search_city.Add(new CityViewModel{cityID=0,cityName="Select City"});

            //set advanced search view model
            vm.Advanced_Search = Set_Advanced_Search();

            return View(vm);
        }

       

        [HttpPost]
        public JsonResult SearchList(string Prefix)
        {
            List<CityViewModel> cityList = DataService.get_cities();

            var result = cityList.Where(x => x.cityName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { CName=x.cityName,CID=x.cityID}).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private AdvancedSearchViewModel Set_Advanced_Search()
        {
            AdvancedSearchViewModel vm_search = new AdvancedSearchViewModel();
           List<CityViewModel> lst_city=DataService.get_cities().OrderBy(x=>x.cityName).ToList();
            List<CityViewModel> search_city = lst_city;
            search_city.Add(new CityViewModel { cityID = 0, cityName = "Select City" });
            vm_search.City_List = new SelectList(search_city, "cityID", "cityName");
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

            vm_search.lst_amenity = DataService.get_Amenities();

            return vm_search;
        }

     
    }
}