using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Controllers;
using HotelAdvice.Filters;


namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AmenityController : BaseController
    {

        public AmenityController(IServiceLayer service)
            : base(service)
        {
           
        }

        // GET: Amenity
        public ActionResult Index(int?page,string filter=null)
        {
            if (filter != "null")
                ViewBag.filter = filter;

            IPagedList paged_list_amenity = DataService.Get_AmenityList(page, filter);

            if(Request.IsAjaxRequest())
                return (ActionResult)PartialView("_PartialAmenityList", paged_list_amenity);
            else
                return  View(paged_list_amenity);
        }

        [HttpPost]
        [ModelValidator]
        public ActionResult ADD_New_Amenity(string amenity_name, string amenity_id,int?page)
        {

            DataService.Post_AddNewAmenity(amenity_name, amenity_id, page);

            string current_page = page.HasValue ? page.Value.ToString() : "1";
            string CurrentFilter = String.IsNullOrEmpty(ViewBag.filter) ? "" : ViewBag.filter.ToString();

            return Json(new { msg = "Amenity inserted successfully!", ctrl = "/Admin/Amenity", cur_pg = current_page, filter = CurrentFilter });
        }

        public ActionResult Delete_Amenity(int id, int? page, string filter = null)
        {
            AmenityViewModel vm = DataService.Get_DeleteAmenity(id, page, filter);
            
            return PartialView("_PartialDeleteAmenity",vm);
        }

        [HttpPost]
        public ActionResult Delete_Amenity(AmenityViewModel vm)
        {
            DataService.Post_DeleteAmenity(vm);
            return Json(new { msg = "Row deleted successfully!", ctrl = "/Admin/Amenity", cur_pg = vm.CurrentPage, filter = vm.CurrentFilter });
        }
    }
}
