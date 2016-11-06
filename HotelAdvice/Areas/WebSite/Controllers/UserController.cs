using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using HotelAdvice.Helper;
using HotelAdvice.App_Code;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;

namespace HotelAdvice.Areas.WebSite.Controllers
{

    public class UserController : Controller
    {
      
        const int defaultPageSize_userpage = 3;
        const int defaultPageSize_searchpage = 3;
        const int defaultPageSize_reviewpage = 4;
        
        IDataRepository db;

        public UserController(IDataRepository repo)
        {
            db = repo;
        }


        [Authorize(Roles = "PublicUser")]
        public ActionResult Index(int ?page,string tab)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            UserPageViewModel vm = new UserPageViewModel();

            vm.lst_wishList = (db.get_wishList(User.Identity.GetUserId())).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
            vm.lst_rating = (db.get_ratingList(User.Identity.GetUserId())).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
            vm.lst_reviews = (db.get_reviewList(User.Identity.GetUserId())).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);

            if (Request.IsAjaxRequest())
            {
                switch (tab)
                {
                    case "wishlist":
                        return PartialView("_PartialWishList", vm.lst_wishList);
                    case "rating":
                        return PartialView("_PartialRatingList", vm.lst_rating);
                    case "review":
                        return PartialView("_PartialYourReviewList", vm.lst_reviews);
                    default:
                        return PartialView("_PartialWishList", vm.lst_wishList);
                }
            }
            else
                return View(vm);
        }


        public ActionResult Reviews(int id,int ?page)
        {
            ReviewPageViewModel vm = db.get_review_page(id);

            AddReviewViewModel your_review =db.get_previous_review(id, User.Identity.GetUserId());
              
            //this is new review
            if (your_review == null)
            {
                your_review = new AddReviewViewModel();
                your_review.RateId = 0;
                your_review.HotelId = id;
                your_review.UserId = User.Identity.GetUserId();
            }
            vm.YourReview = your_review;

            int current_page_index = page.HasValue ? page.Value : 1;

             ApplicationUserManager userMgr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
             vm.lst_reviews = db.get_reviews_for_hotel(id, userMgr).ToPagedList(current_page_index, defaultPageSize_reviewpage);

             if (Request.IsAjaxRequest())
                 return PartialView("_PartialHotelReviewList", vm.lst_reviews);
             else
                 return View(vm);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Review(AddReviewViewModel vm)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }
            else
            {
                db.add_review(vm);

                if (vm.fromProfilePage)
                {
                    IPagedList<HotelSearchViewModel> model = db.get_reviewList(User.Identity.GetUserId()).ToPagedList<HotelSearchViewModel>(vm.currentPageIndex, defaultPageSize_userpage);
                    string partialview = RenderPartial.RenderRazorViewToString(this
                         , "~/Areas/WebSite/views/User/_PartialYourReviewList.cshtml"
                         , model);

                    return Json(new { msg = "success_edit_review", partial = partialview });

                }
                else
                    return Json(new { msg = "success_add_review" });
            }
        }


        [HttpGet]
        public ActionResult EditReview(int id,int?page)
        {

           AddReviewViewModel vm= db.get_previous_review(id, User.Identity.GetUserId());
           vm.fromProfilePage = true;
           vm.currentPageIndex = page.HasValue ? page.Value : 1;

           return PartialView("_PartialAddReview", vm);
        }
    
        [HttpGet]
        public ActionResult DeleteReview(int id, int? page)
        {
            DeleteReviewViewModel vm=new DeleteReviewViewModel();

            vm.hotelId = id;
            vm.currentPageIndex = page.HasValue ? page.Value : 1;
            vm.HotelName = db.get_hotel_byId(id).HotelName;
            vm.UserId = User.Identity.GetUserId();

            return PartialView("_PartialDeleteReview",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(DeleteReviewViewModel vm)
        {

            db.delete_review(vm.hotelId,vm.UserId);
            int currentPageIndex = vm.currentPageIndex;

            List<HotelSearchViewModel> lst_reviews = db.get_reviewList(User.Identity.GetUserId());
            if (currentPageIndex > 1 && lst_reviews.Count() < currentPageIndex * defaultPageSize_userpage)
                currentPageIndex = currentPageIndex - 1;

            return PartialView("_PartialYourReviewList", lst_reviews.ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage));

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
        public ActionResult RateHotel(int hotel_id, int your_rating, int? page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }

            db.rate_hotel(hotel_id, User.Identity.GetUserId(), your_rating);

            if (Request.IsAjaxRequest())
            {
                int currentPageIndex = page.HasValue ? page.Value : 1;
                IPagedList<HotelSearchViewModel> model =  db.get_ratingList(User.Identity.GetUserId()).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
               string partialview = RenderPartial.RenderRazorViewToString(this
                    , "~/Areas/WebSite/views/User/_PartialRatingList.cshtml"
                    , model);

               return Json(new { msg = "rating_success",partial=partialview});

            }
            else
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