using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.ViewModels;

namespace HotelAdvice.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ADD_New_City(int id)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = id;
            return PartialView("_PartialAddCity",vm);
        }

        //[HttpPost]
        //public ActionResult ADD_New_City(int id)
        //{
        //    CityViewModel vm = new CityViewModel();
        //    vm.cityID = id;
        //    return PartialView("_PartialAddCity", vm);
        //}

    }
}