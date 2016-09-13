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


        #region Restaurant
        [HttpPost]
        public JsonResult Get_Restaurants(string Prefix)
        {
            List<tbl_Restuarant> restList = db.get_restaurants();

            var result = restList.Where(x => x.RestaurantName.ToLower().Contains(Prefix.ToLower()))
                .Select(x => new { RestName = x.RestaurantName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        #endregion Restaurant

    }
}