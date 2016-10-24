using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HotelAdvice.Helper;
using HotelAdvice.App_Code;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;

namespace HotelAdvice.Areas.WebSite.Controllers
{
       

    public class UserController : Controller
    {
        DAL db = new DAL();
        const int defaultPageSize = 9;

        // GET: WebSite/User
        public ActionResult Index()
        {
            UserPageViewModel vm = new UserPageViewModel();

            vm.lst_wishList=db.get_wishList(User.Identity.GetUserId()).ToPagedList<HotelSearchViewModel>(1,defaultPageSize);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavorite(int hotel_id, int city_id, int? page, string sort)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }
            else
            {

                int result = db.add_favorite_hotel(hotel_id, User.Identity.GetUserId());

                string partialview = RenderPartial.RenderViewToString(this.ControllerContext
                    , "~/Areas/WebSite/views/SearchHotel/_PartialHotelListResults.cshtml"
                    , SetPartialHotelResult(city_id, page, sort));

                if (result == 1)
                    return Json(new { msg = "add_favorite_success", partial = partialview });
                else
                    return Json(new { msg = "favorite_already_exist", partial = partialview });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavorite_Detail(int hotel_id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }
            else
            {

                int result = db.add_favorite_hotel(hotel_id, User.Identity.GetUserId());

                if (result == 1)
                    return Json(new { msg = "add_favorite_success" });
                else
                    return Json(new { msg = "favorite_already_exist" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateHotel(int hotel_id, int your_rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });

            }
            return Json(new { msg = "add_success" });
        }

        private IPagedList<HotelSearchViewModel> SetPartialHotelResult(int city_id, int? page, string sort)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            //get hotel list in this city_id
            List<HotelSearchViewModel> lst_hotels = db.Search_Hotels_in_city(city_id, User.Identity.GetUserId());

            //sort
            switch (sort)
            {
                case "distance":
                    lst_hotels = lst_hotels.OrderBy(x => x.distance_citycenter).ToList();
                    break;

                case "rating":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.GuestRating).ToList();
                    break;
                case "5to1":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.HotelStars).ToList();
                    break;
                case "1to5":
                    lst_hotels = lst_hotels.OrderBy(x => x.HotelStars).ToList();
                    break;
                default:
                    break;

            }

            //pagination

            return lst_hotels.ToPagedList(currentPageIndex, defaultPageSize);
        }


    }
}