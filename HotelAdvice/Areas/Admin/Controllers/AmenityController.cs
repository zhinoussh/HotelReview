using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using HotelAdvice.DataAccessLayer;


namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AmenityController : Controller
    {
        IDataRepository db;
        private const int defaultPageSize = 10;

        public AmenityController(IDataRepository repo)
        {
            db = repo;
        }

        // GET: Amenity
        public ActionResult Index(int?page,string filter=null)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            List<AmenityViewModel> lst_amenity=db.get_Amenities();
            if (filter != "null")
                ViewBag.filter = filter;

            if (!String.IsNullOrEmpty(filter) && filter!="null")
            {

                filter = filter.ToLower();
                lst_amenity = lst_amenity.Where(x => x.AmenityName.ToLower().Contains(filter)).ToList();
            }

            lst_amenity = lst_amenity.OrderBy(x => x.AmenityName).Select((x, index) => new AmenityViewModel
            {
                RowNum = index + 1,
                AmenityName = x.AmenityName,
                AmenityID = x.AmenityID
            }).ToList();

            IPagedList paged_list = lst_amenity.ToPagedList(currentPageIndex, defaultPageSize);

            return  Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_PartialAmenityList", paged_list)
                : View(paged_list);
        }

        [HttpPost]
        public ActionResult ADD_New_Amenity(string amenity_name, string amenity_id,int?page)
        {
            int id = String.IsNullOrEmpty(amenity_id) ? 0 : Int32.Parse(amenity_id.ToString());
            db.add_amenity(id, amenity_name);
            string current_page = page.HasValue ? page.Value.ToString() : "1";
            string CurrentFilter = String.IsNullOrEmpty(ViewBag.filter) ? "" : ViewBag.filter.ToString();

            return Json(new { msg = "Amenity inserted successfully!", ctrl = "/Admin/Amenity", cur_pg = current_page, filter = CurrentFilter });
        }

        public ActionResult Delete_Amenity(int id, int? page, string filter = null)
        {
            AmenityViewModel vm = new AmenityViewModel();
            vm.AmenityID = id;
            vm.CurrentFilter = String.IsNullOrEmpty(filter) ? "" : filter.ToString();
            vm.CurrentPage = page.HasValue ? page.Value : 1;
            
            return PartialView("_PartialDeleteAmenity",vm);
        }

        [HttpPost]
        public ActionResult Delete_Amenity(AmenityViewModel vm)
        {
            db.delete_Amenity(vm.AmenityID);

            return Json(new { msg = "Row deleted successfully!", ctrl = "/Admin/Amenity", cur_pg = vm.CurrentPage, filter = vm.CurrentFilter });
        }
    }
}
