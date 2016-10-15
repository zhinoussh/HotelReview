using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.App_Code;
using PagedList;

namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : Controller
    {
        private const int defaultPageSize = 10;
        DAL db = new DAL();

        // GET: City
        public ActionResult Index(int? page, string filter = null)
        {
            ViewBag.filter = filter;
            int currentPageIndex = page.HasValue ? page.Value : 1;

           List<CityViewModel> lst_cities = db.get_cities();
           foreach (CityViewModel c in lst_cities)
               if(c.cityAttractions!=null)
                   c.cityAttractions = (c.cityAttractions.Length < 100 ? c.cityAttractions : (c.cityAttractions.Substring(0, 100) + "..."));

           if (!String.IsNullOrEmpty(filter))
           {
               filter = filter.ToLower();
               lst_cities = lst_cities.Where(x => x.cityName.ToLower().Contains(filter)).ToList();
           }

           //sort and set row number
           lst_cities = lst_cities.OrderBy(x => x.cityName).Select((x, index) => new CityViewModel
           {
               RowNum = index + 1,
               cityID = x.cityID,
               cityName = x.cityName,
               cityAttractions = x.cityAttractions             
           }).ToList();

            
            IPagedList paged_list_city = lst_cities.ToPagedList(currentPageIndex, defaultPageSize);

           return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("_PartialCityList", paged_list_city)
               : View(paged_list_city);
        }

        [HttpGet]
        public ActionResult ADD_New_City(int ?id,int? page, string filter = null)
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

            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";
           

            return PartialView("_PartialAddCity",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADD_New_City(CityViewModel city)
        {
            if (ModelState.IsValid)
            {
                db.add_city(city.cityID, city.cityName, city.cityAttractions);
                return Json(new { msg = "The city inserted successfully.", ctrl = "/Admin/City", cur_pg = city.CurrentPage, filter = city.CurrentFilter + "" });
            }
            else
                return PartialView("_PartialAddCity", city);
        }


        [HttpGet]
        public ActionResult CityDescription(int id)
        {
            List<string> city_prop = db.get_city_byId(id);
           CityViewModel vm = new CityViewModel();
           vm.cityName = city_prop[0];
           vm.cityAttractions = city_prop[1];
           return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_City(int id, int? page, string filter = null)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = id;
            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";
           
            return PartialView("_PartialDeleteCity", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_City(CityViewModel city)
        {
            db.delete_city(city.cityID);
            return Json(new { msg = "Row is deleted successfully!", ctrl = "/Admin/City", cur_pg = city.CurrentPage, filter = city.CurrentFilter + "" });
        }

    }
}