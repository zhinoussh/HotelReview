using HotelAdvice.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.ViewModels;
using PagedList;

namespace HotelAdvice.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HotelController : Controller
    {
        private const int defaultPageSize = 5;
        DAL db = new DAL();

        // GET: Hotel
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            List<HotelViewModel> lst_hotels = db.get_hotels();
            IPagedList paged_list = lst_hotels.ToPagedList(currentPageIndex, defaultPageSize);

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_PartialHotelList", paged_list)
                : View(paged_list);
        }

        [HttpGet]
        public ActionResult ADD_New_Hotel(int? id)
        {
            HotelViewModel vm = new HotelViewModel();
            int HotelId = Int32.Parse(id == null ? "0" : id + "");
            vm.HotelId = HotelId;

            List<CityViewModel> cities = db.get_cities();
            vm.lst_city = new SelectList(cities, "cityID", "cityName");
            vm.CityId = cities.First().cityID;


            //this is edit
            if (id != null)
            {
                List<string> Hotel_prop = db.get_hotel_byId(HotelId);
                vm.HotelName = Hotel_prop[0];
                vm.Description = Hotel_prop[1];
                vm.CityId = Int32.Parse(Hotel_prop[2]);

            }

            return PartialView("_PartialAddHotel", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADD_New_Hotel(HotelViewModel Hotel)
        {
            if (ModelState.IsValid)
            {
                db.add_hotel(Hotel.HotelId, Hotel.HotelName, Hotel.Description,Hotel.CityId);
                return Json(new { msg = "The Hotel inserted successfully." });
            }
            else
                return PartialView("_PartialAddHotel", Hotel);
        }


        [HttpGet]
        public ActionResult HotelDescription(int id)
        {
            List<string> Hotel_prop = db.get_hotel_byId(id);
            HotelViewModel vm = new HotelViewModel();
            vm.HotelName = Hotel_prop[0];
            vm.Description = Hotel_prop[1];
            return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_Hotel(int id)
        {
            HotelViewModel vm = new HotelViewModel();
            vm.HotelId = id;

            return PartialView("_PartialDeleteHotel", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Hotel(HotelViewModel Hotel)
        {
            db.delete_hotel(Hotel.HotelId);
            return Json(new { msg = "Row is deleted successfully!" });
        }

    }
}