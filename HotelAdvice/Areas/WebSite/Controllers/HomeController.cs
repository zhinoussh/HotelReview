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
            HomeViewModel vm = DataService.Get_HomePage();

            return View(vm);
        }

        [HttpPost]
        public JsonResult SearchList(string Prefix)
        {
            List<CityViewModel> cityList = DataService.DataLayer.get_cities();

            var result = cityList.Where(x => x.cityName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { CName=x.cityName,CID=x.cityID}).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

     

     
    }
}