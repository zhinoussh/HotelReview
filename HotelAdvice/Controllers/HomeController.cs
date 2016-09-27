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

            List<string> lst_locations = new List<string>();
            lst_locations.Add("Any Kilometers");
            lst_locations.Add("less than 1 km");
            lst_locations.Add("less than 2 km");
            lst_locations.Add("less than 3 km");
            lst_locations.Add("less than 4 km");
            lst_locations.Add("less than 5 km");

            
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

     
    }
}