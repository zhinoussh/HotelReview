using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.App_Code;
using PagedList;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Controllers;
using HotelAdvice.Filters;

namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CityController : BaseController
    {
        private const int defaultPageSize = 10;
        IDataRepository db;

        public CityController(IServiceLayer service)
            : base(service)
        {

        }

        public ActionResult Index(int? page, string filter = null)
        {
            ViewBag.filter = filter;
            
          IPagedList  paged_list_city = DataService.Get_CityList(page, filter);

           return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("_PartialCityList", paged_list_city)
               : View(paged_list_city);
        }

        [HttpGet]
        public ActionResult ADD_New_City(int ?id,int? page, string filter = null)
        {
            CityViewModel vm = DataService.Get_AddNewCity(id, page, filter);

            return PartialView("_PartialAddCity",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelValidator]
        public ActionResult ADD_New_City(CityViewModel city)
        {
            //if (ModelState.IsValid)
            //{
                DataService.Post_AddNewCity(city);
                return Json(new { msg = "The city inserted successfully.", ctrl = "/Admin/City", cur_pg = city.CurrentPage, filter = city.CurrentFilter + "" });
            //}
            //else
            //    return PartialView("_PartialAddCity", city);
        }


        [HttpGet]
        public ActionResult CityDescription(int id)
        {
          CityViewModel vm=  DataService.GetCityDescription(id);
           return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_City(int id, int? page, string filter = null)
        {
            CityViewModel vm = DataService.Get_DeleteCity(id, page, filter);
            return PartialView("_PartialDeleteCity", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_City(CityViewModel city)
        {
            DataService.Post_DeleteCity(city);
            return Json(new { msg = "Row is deleted successfully!", ctrl = "/Admin/City", cur_pg = city.CurrentPage, filter = city.CurrentFilter + "" });
        }

    }
}