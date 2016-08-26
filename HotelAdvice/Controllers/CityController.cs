using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.ViewModels;
using HotelAdvice.App_Code;

namespace HotelAdvice.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : Controller
    {
        DAL db = new DAL();

        // GET: City
        public ActionResult Index()
        {
           List<CityViewModel> lst_cities = db.get_cities();
           foreach (CityViewModel c in lst_cities)
                c.cityAttractions = (c.cityAttractions.Length < 100 ? c.cityAttractions : (c.cityAttractions.Substring(0, 100) + "..."));
           return View(lst_cities);
        }

        [HttpGet]
        public ActionResult ADD_New_City(int ?id)
        {
            CityViewModel vm = new CityViewModel();
            int cityId = Int32.Parse(id == null ? "0" : id + "");
            vm.cityID = cityId;

            //this is edit
            if (id != null)
            {
                List<string> city_prop = db.get_city_byId(cityId);
                vm.cityName = city_prop[0];
                vm.cityAttractions = city_prop[1];
            }
            
            return PartialView("_PartialAddCity",vm);
        }


        [HttpPost]
        public ActionResult ADD_New_City(CityViewModel city)
        {
            db.add_city(city.cityID, city.cityName, city.cityAttractions);
            return Json(new { msg="The city inserted successfully." });
        }

        [HttpGet]
        public ActionResult CityDescription(int city_id)
        {
           List<string> city_prop= db.get_city_byId(city_id);
           CityViewModel vm = new CityViewModel();
           vm.cityName = city_prop[0];
           vm.cityAttractions = city_prop[1];
           return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_City(int id)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = id;

            return PartialView("_PartialDeleteCity", vm);
        }


        [HttpPost]
        public ActionResult Delete_City(CityViewModel city)
        {
            db.delete_city(city.cityID);
            return Json(new {msg="Row is deleted successfully!" });
        }

    }
}