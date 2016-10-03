using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.Models;
using HotelAdvice.ViewModels;
using HotelAdvice.App_Code;
using System.IO;

namespace HotelAdvice.Controllers
{
    public class HomeController : Controller
    {
        DAL db = new DAL();
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HomeViewModel vm = new HomeViewModel();
            vm.lst_city = db.get_cities().OrderBy(x=>x.cityName).ToList();

            List<CityViewModel> search_city=vm.lst_city;
            search_city.Add(new CityViewModel{cityID=0,cityName="Select City"});

            vm.City_List = new SelectList(search_city, "cityID", "cityName");
            vm.selected_city = 0;

            List<KeyValuePair<int, string>> lst_locations = new List<KeyValuePair<int, string>>();
            lst_locations.Add(new KeyValuePair<int, string>(0, "Any Kilometer"));
            lst_locations.Add(new KeyValuePair<int, string>(1, "less than 1 km"));
            lst_locations.Add(new KeyValuePair<int, string>(2, "less than 2 km"));
            lst_locations.Add(new KeyValuePair<int, string>(3, "less than 3 km"));
            lst_locations.Add(new KeyValuePair<int, string>(4, "less than 4 km"));
            lst_locations.Add(new KeyValuePair<int, string>(5, "less than 5 km"));
            lst_locations.Add(new KeyValuePair<int, string>(10, "less than 10 km"));
            lst_locations.Add(new KeyValuePair<int, string>(20, "less than 20 km"));
            vm.Location = new SelectList(lst_locations, "Key", "Value");
            vm.distance_city_center = 0;
            vm.distance_airport = 0;

            vm.lst_amenity = db.get_Amenities();
            return View(vm);
        }

        public ActionResult About()
        {

            return View();
        }
        
        [HttpPost]
        public JsonResult SearchList(string Prefix)
        {
            List<CityViewModel> cityList = db.get_cities();

            var result = cityList.Where(x => x.cityName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { CName=x.cityName,CID=x.cityID}).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Advanced_Search(HomeViewModel vm, string slider_guest_review)
        {
            //db.Search_Hotels();
            return Json(new { result="success!"});
        }

     
    }
}