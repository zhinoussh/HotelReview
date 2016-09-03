using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.Models;
using HotelAdvice.ViewModels;
using HotelAdvice.App_Code;

namespace HotelAdvice.Controllers
{
    public class HomeController : Controller
    {
        DAL db = new DAL();
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
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