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
            HotelViewModel vm = new HotelViewModel();
            int HotelId = Int32.Parse(id == null ? "0" : id + "");
            vm.HotelId = HotelId;

            List<CityViewModel> cities = DataService.get_cities();
            vm.CityId = cities.First().cityID;
            vm.imgPath = "/images/empty.gif?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt") ;
            //this is edit
            if (HotelId != 0)
            {
                vm = DataService.get_hotel_byId(HotelId);
               
                string path= "/Upload/" + vm.HotelName + "/main.jpg";
                if (System.IO.File.Exists(Server.MapPath(@path)))
                    vm.imgPath = path+"?"+ DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                else
                    vm.imgPath = "/images/empty.gif?"+ DateTime.Now.ToString("ddMMyyyyhhmmsstt");  

                //add restaurants
                List<tbl_Restuarant> lst_rest=DataService.get_hotel_restaurants(HotelId);
                if (lst_rest.Count > 0)
                {
                    vm.restaurants = string.Join(",", lst_rest.Select(x=>x.RestaurantName));
                }

                //add rooms
                List<tbl_room_type> lst_room = DataService.get_hotel_rooms(HotelId);
                if (lst_room.Count > 0)
                {
                    vm.rooms = string.Join(",", lst_room.Select(x => x.Room_Type));
                }


                //add sightseeings
                List<tbl_sightseeing> lst_sightseeing = DataService.get_hotel_sightseeings(HotelId);
                if (lst_sightseeing.Count > 0)
                {
                    vm.sightseeing = string.Join(",", lst_sightseeing.Select(x => x.Sightseeing_Type));
                }
            }

            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";
            vm.lst_city = new SelectList(cities, "cityID", "cityName");
            vm.amenities = DataService.get_hotel_amenities(HotelId);

            return PartialView("_PartialAddHotel", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADD_New_Hotel(HotelViewModel Hotel)
        {
            if (ModelState.IsValid)
            {
                Hotel.distance_citycenter = Hotel.distance_citycenter!=null ?Hotel.distance_citycenter : 0;
                Hotel.distance_airport = Hotel.distance_airport!=null ? Hotel.distance_airport : 0;

                string original_hotel_name ="";
                if(Hotel.HotelId!=0)
                    original_hotel_name = DataService.get_hotel_byId(Hotel.HotelId).HotelName;

                //save hotel
                DataService.add_hotel(Hotel);

                //set a folder for hotel
                string hotel_dir = Server.MapPath(@"~\Upload\" + Hotel.HotelName);
                if (Hotel.HotelId == 0)
                {
                    if (!Directory.Exists(hotel_dir))
                        Directory.CreateDirectory(hotel_dir);
                }
                //this is edit
                else
                {
                    if (original_hotel_name.ToLower() != Hotel.HotelName.ToLower())
                    {
                        string pre_hotel_dir = Server.MapPath(@"~\Upload\" + original_hotel_name);
                        Directory.Move(pre_hotel_dir, hotel_dir);
                    }
                }

                //save image
                if (Hotel.PhotoFile != null)
                {
                    HttpPostedFileBase file = Hotel.PhotoFile;
                    var filename = file.FileName;
                    file.SaveAs(hotel_dir + "\\main.jpg");
                }

                return Json(new { msg = "The Hotel inserted successfully.", ctrl = "/Admin/Hotel", cur_pg = Hotel.CurrentPage, filter = Hotel.CurrentFilter + "" });
            }
            else
                return PartialView("_PartialAddHotel", Hotel);

        }

        [HttpGet]
        public ActionResult HotelDescription(int id)
        {
            HotelViewModel  vm = DataService.get_hotel_byId(id);
            return PartialView("_PartialDescription", vm);
        }

        [HttpGet]
        public ActionResult Delete_Hotel(int id, int? page, string filter = null)
        {
            HotelViewModel vm = new HotelViewModel();
            vm.HotelId = id;
            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";
           
            return PartialView("_PartialDeleteHotel", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Hotel(HotelViewModel Hotel)
        {
            string hotelName = DataService.get_hotel_byId(Hotel.HotelId).HotelName;
            if (!String.IsNullOrEmpty(hotelName))
            {
                string hotel_dir = Server.MapPath(@"~\Upload\" + hotelName);

                DataService.delete_hotel(Hotel.HotelId);

                if (Directory.Exists(hotel_dir))
                    Directory.Delete(hotel_dir, true);
            }
            return Json(new { msg = "Row is deleted successfully!", ctrl = "/Admin/Hotel", cur_pg = Hotel.CurrentPage, filter = Hotel.CurrentFilter + "" });
        }

         #endregion Hotel

         #region Hotel_Image
       
        [HttpGet]
        public ActionResult HotelImage(int id)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();
            vm.HotelName=DataService.get_hotel_byId(id).HotelName;
            string hotel_dir = Server.MapPath(@"~\Upload\" + vm.HotelName);
            if(Directory.Exists(hotel_dir))
                vm.uploaded_images = Directory.GetFiles(hotel_dir).Select(x => Path.GetFileName(x)).Where(x=>x!="main.jpg").ToArray();
            vm.HotelId = id;

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
            HttpPostedFileBase item = vm.image;

            if (item != null && hotel_ID != 0)
            {
                string file_name = DataService.save_hotel_image(hotel_ID);
                if (!String.IsNullOrEmpty(file_name))
                {
                    string hotel_name = file_name.Substring(0, file_name.LastIndexOf('_'));

                    string hotel_dir = Server.MapPath(@"~\Upload\" + hotel_name);
                    if (!Directory.Exists(hotel_dir))
                        Directory.CreateDirectory(hotel_dir);

                    item.SaveAs(hotel_dir + "\\" + file_name);
                }
            }


            return Json(new { msg = "images were uploaded successfully!" });
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
            DataService.delete_hotel_image(photo.PhotoName);
            string file_path = Server.MapPath(@"\Upload\" + photo.PhotoName.Substring(0, photo.PhotoName.LastIndexOf('_')) + "\\" + photo.PhotoName);
            if (System.IO.File.Exists(file_path))
                System.IO.File.Delete(file_path);

            return Json(new { msg = "Row is deleted successfully!" });
        }

        public ActionResult Show_HotelPhoto(string photo_name)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();
            vm.PhotoName =  photo_name.Substring(0, photo_name.LastIndexOf('_')) + "\\" + photo_name;

            return PartialView("_PartialShowImage", vm);
        }

         #endregion Hotel_Image


        [HttpPost]
        public JsonResult Get_Restaurants(string Prefix)
        {
            List<tbl_Restuarant> restList = DataService.get_restaurants();

            var result = restList.Where(x => x.RestaurantName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { RestName = x.RestaurantName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
         public JsonResult Get_Rooms(string Prefix)
         {
             List<tbl_room_type> RoomList = DataService.get_roomTypes();

             var result = RoomList.Where(x => x.Room_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { RoomType = x.Room_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }


         [HttpPost]
         public JsonResult Get_SightSeeing(string Prefix)
         {
             List<tbl_sightseeing> SightList = DataService.get_Sightseeing();

             var result = SightList.Where(x => x.Sightseeing_Type.ToLower().Contains(Prefix.ToLower()))
                 .Select(x => new { SightSeeing = x.Sightseeing_Type }).ToList();

             return Json(result, JsonRequestBehavior.AllowGet);
         }
        
    }
}