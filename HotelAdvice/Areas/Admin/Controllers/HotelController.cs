using HotelAdvice.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.Admin.Models;
using PagedList;
using System.IO;
using HotelAdvice.Controllers;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Filters;
using HotelAdvice.Helper;

namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HotelController : BaseController
    {
        
        public HotelController(IServiceLayer service)
            : base(service)
        {

        }

        #region Hotel

        // GET: Hotel
        public ActionResult Index(int? page, string filter = null)
        {
            ViewBag.filter = filter;

            IPagedList paged_list_hotels = DataService.Get_HotelList(page, filter);

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("_PartialHotelList", paged_list_hotels)
                : View(paged_list_hotels);
        }

        [HttpGet]
        public ActionResult ADD_New_Hotel(int? id, int? page,string filter=null)
        {
            HotelViewModel vm = DataService.Get_AddNewHotel(this,id, page, filter);

            return PartialView("_PartialAddHotel", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelValidator]
        public ActionResult ADD_New_Hotel(HotelViewModel Hotel)
        {
            //if (ModelState.IsValid)
            //{
                DataService.Post_AddNewHotel(Hotel, this);
                return Json(new { msg = "The Hotel inserted successfully.", ctrl = "/Admin/Hotel", cur_pg = Hotel.CurrentPage, filter = Hotel.CurrentFilter + "" });
            //}
            //else
            //{
            //    List<CityViewModel> cities = DataService.DataLayer.get_cities();
            //    Hotel.lst_city = new SelectList(cities, "cityID", "cityName");
            //    return PartialView("_PartialAddHotel", Hotel);
            //}

        }

        [HttpGet]
        public ActionResult HotelDescription(int id)
        {
            HotelViewModel  vm = DataService.GetHotelDescription(id);

            return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_Hotel(int id, int? page, string filter = null)
        {
            HotelViewModel vm = DataService.Get_DeleteHotel(id,page,filter);
            return PartialView("_PartialDeleteHotel", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Hotel(HotelViewModel Hotel)
        {
            DataService.Post_DeleteHotel(Hotel, this);

            return Json(new { msg = "Row is deleted successfully!", ctrl = "/Admin/Hotel", cur_pg = Hotel.CurrentPage, filter = Hotel.CurrentFilter + "" });
        }

         #endregion Hotel

         #region Hotel_Image
       
        [HttpGet]
        public ActionResult HotelImages(int id)
        {
            HotelImagesViewModel vm = DataService.Get_HotelImagesView(id, this);
                                       
            return View(vm);           
        }                              
                                       
        [HttpPost]
        public ActionResult DeleteMainImage(HotelImagesViewModel vm, int hotel_ID)
        {
            
            return Json(new { msg = "images were uploaded successfully!" });
        }


        [HttpPost]
        public ActionResult AddImage(HotelImagesViewModel vm, int hotel_ID)
        {
            DataService.Post_AddHotelPhoto(vm, hotel_ID,this);

            return Json(new { msg = "image uploaded successfully!" });
        }

        [HttpGet]
        public ActionResult Delete_HotelPhoto(string photo_name)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();
            vm.PhotoName = photo_name;

            return PartialView("_PartialDeletePhoto", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_HotelPhoto(HotelImagesViewModel photo)
        {
            DataService.Post_DeleteHotelPhoto(photo,this);
            return Json(new { msg = "Row is deleted successfully!" });
        }

        public ActionResult Show_HotelPhoto(string photo_name)
        {
            HotelImagesViewModel vm = DataService.Get_HotelPhoto(photo_name);

            return PartialView("_PartialShowImage", vm);
        }

         #endregion Hotel_Image


        [HttpPost]
        public JsonResult Get_Restaurants(string Prefix)
        {
            List<tbl_Restuarant> restList = DataService.DataLayer.get_restaurants();
                

            var result = restList.Where(x => x.RestaurantName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { RestName = x.RestaurantName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
         public JsonResult Get_Rooms(string Prefix)
         {
             List<tbl_room_type> RoomList = DataService.DataLayer.get_roomTypes();

             var result = RoomList.Where(x => x.Room_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { RoomType = x.Room_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public JsonResult Get_SightSeeing(string Prefix)
         {
             List<tbl_sightseeing> SightList = DataService.DataLayer.get_Sightseeing();

             var result = SightList.Where(x => x.Sightseeing_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { SightSeeing = x.Sightseeing_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }
        
    }
}