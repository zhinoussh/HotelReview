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

        public ActionResult Index(string returnUrl,int?page,string tab)
        {
            ViewBag.ReturnUrl = returnUrl;
            HomeViewModel vm = DataService.Get_HomePage(page);

            if (Request.IsAjaxRequest())
            {
                switch (tab)
                {
                    case "citylist":
                        return PartialView("_PartialCityList",vm.lst_city);
                    case "popularlist":
                        return PartialView("_PartialPopularHotels", vm.lst_poupular_hotels);
                    case "toplist":
                        return PartialView("_PartialTopHotels", vm.lst_top_hotels);
                    default:
                        return PartialView("_PartialCityList", vm.lst_city);
                }
            }
            else
                return View(vm);
        }

        [HttpPost]
        public JsonResult SearchList(string Prefix)
        {
            List<string> result=DataService.search_destinations_by_prefix(Prefix);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
     
    }
}