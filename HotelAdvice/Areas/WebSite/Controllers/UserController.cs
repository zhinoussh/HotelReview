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
        const int defaultPageSize_userpage = 6;
        const int defaultPageSize_searchpage = 3;

        [Authorize(Roles = "PublicUser")]
        public ActionResult Index(int ?page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            UserPageViewModel vm = new UserPageViewModel();

            vm.lst_wishList = (db.get_wishList(User.Identity.GetUserId())).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);

            if (Request.IsAjaxRequest())
                return PartialView("_PartialWishList", vm.lst_wishList);
            else
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

                IPagedList<HotelSearchViewModel> model=SetPartialHotelResult(city_id, page, sort);
                string partialview = RenderPartial.RenderRazorViewToString(this
                    , "~/Areas/WebSite/views/SearchHotel/_PartialHotelListResults.cshtml"
                    , model);

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
        public ActionResult DeleteFavorite(int hotel_id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            db.remove_favorite_hotel(hotel_id, User.Identity.GetUserId());

            List<HotelSearchViewModel> lst_wishlist = db.get_wishList(User.Identity.GetUserId());
            if (currentPageIndex>1 && lst_wishlist.Count() < currentPageIndex * defaultPageSize_userpage)
                currentPageIndex = currentPageIndex - 1;

            return PartialView("_PartialWishList", lst_wishlist.ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateHotel(int hotel_id, int your_rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });

            }
           db.rate_hotel(hotel_id, User.Identity.GetUserId(), your_rating);

            return Json(new { msg = "rating_success" });
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

            return lst_hotels.ToPagedList(currentPageIndex, defaultPageSize_searchpage);
        }


    }
}