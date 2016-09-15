using HotelAdvice.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelAdvice.ViewModels;
using HotelAdvice.Models;
using PagedList;
using System.IO;

namespace HotelAdvice.Controllers
{
    [Authorize(Roles = "Administrator")]
   
    
    public class HotelController : Controller
    {
        private const int defaultPageSize = 5;
        DAL db = new DAL();

        #region Hotel

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
            vm.CityId = cities.First().cityID;
            vm.imgPath = "/images/empty.gif";
            //this is edit
            if (HotelId != 0)
            {
                vm = db.get_hotel_byId(HotelId);
               
                string path= "/Upload/" + vm.HotelName + "/main.jpg";
                if (System.IO.File.Exists(Server.MapPath(@path)))
                    vm.imgPath = path;    
                else
                    vm.imgPath = "/images/empty.gif";   

                //add restaurants
                List<tbl_Restuarant> lst_rest=db.get_hotel_restaurants(HotelId);
                if (lst_rest.Count > 0)
                {
                    vm.restaurants = string.Join(",", lst_rest.Select(x=>x.RestaurantName));
                }

                //add rooms
                List<tbl_room_type> lst_room = db.get_hotel_rooms(HotelId);
                if (lst_room.Count > 0)
                {
                    vm.rooms = string.Join(",", lst_room.Select(x => x.Room_Type));
                }

                //add amenities
                List<tbl_amenity> lst_amenity = db.get_hotel_amenities(HotelId);
                if (lst_amenity.Count > 0)
                {
                    vm.amenities = string.Join(",", lst_amenity.Select(x => x.AmenityName));
                }

                //add sightseeings
                List<tbl_sightseeing> lst_sightseeing = db.get_hotel_sightseeings(HotelId);
                if (lst_sightseeing.Count > 0)
                {
                    vm.sightseeing = string.Join(",", lst_sightseeing.Select(x => x.Sightseeing_Type));
                }
            }

            vm.lst_city = new SelectList(cities, "cityID", "cityName");

            return PartialView("_PartialAddHotel", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADD_New_Hotel(HotelViewModel Hotel)
        {
            if (ModelState.IsValid)
            {
                db.add_hotel(Hotel);

                if (Hotel.PhotoFile != null)
                {
                    HttpPostedFileBase file = Hotel.PhotoFile;
                    string hotel_dir = Server.MapPath(@"~\Upload\" + Hotel.HotelName);
                    if (!Directory.Exists(hotel_dir))
                        Directory.CreateDirectory(hotel_dir);
                    var filename = file.FileName;
                    //var ext = filename.Substring(filename.LastIndexOf('.'));
                    //filename = "main" + ext;
                    file.SaveAs(hotel_dir + "\\main.jpg");
                }
                return Json(new { msg = "The Hotel inserted successfully."});
            }
            else
                return PartialView("_PartialAddHotel", Hotel);

        }

        [HttpGet]
        public ActionResult HotelDescription(int id)
        {
            HotelViewModel  vm = db.get_hotel_byId(id);
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
        #endregion Hotel


         [HttpPost]
        public JsonResult Get_Restaurants(string Prefix)
        {
            List<tbl_Restuarant> restList = db.get_restaurants();

            var result = restList.Where(x => x.RestaurantName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { RestName = x.RestaurantName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
         public JsonResult Get_Rooms(string Prefix)
         {
             List<tbl_room_type> RoomList = db.get_roomTypes();

             var result = RoomList.Where(x => x.Room_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { RoomType = x.Room_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }


         [HttpPost]
         public JsonResult Get_Amenities(string Prefix)
         {
             List<tbl_amenity> AmenityList = db.get_Amenities();

             var result = AmenityList.Where(x => x.AmenityName.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { Amenity = x.AmenityName }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public JsonResult Get_SightSeeing(string Prefix)
         {
             List<tbl_sightseeing> SightList = db.get_Sightseeing();

             var result = SightList.Where(x => x.Sightseeing_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { Amenity = x.Sightseeing_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }
        
    }
}