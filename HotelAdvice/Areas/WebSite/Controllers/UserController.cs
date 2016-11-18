using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Controllers;
using HotelAdvice.Filters;

namespace HotelAdvice.Areas.WebSite.Controllers
{

    public class UserController : BaseController
    {
      
        public UserController(IServiceLayer service)
            : base(service)
        {
        }

        [Authorize(Roles = "PublicUser")]
        public ActionResult Index(int ?page,string tab)
        {
            UserPageViewModel vm = DataService.Get_UserProfilePage(User.Identity.GetUserId(), page, tab);

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
            ReviewPageViewModel vm = DataService.Get_ReviewPage(User.Identity.GetUserId(),this, id, page);

             if (Request.IsAjaxRequest())
                 return PartialView("_PartialHotelReviewList", vm.lst_reviews);
             else
                 return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelValidator]
        public ActionResult Add_Review(AddReviewViewModel vm)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }
            else
            {
                DataService.Post_AddNewReview(vm);
                
                if (vm.fromProfilePage)
                {
                    string PartialReview = DataService.Get_PartialReviewList(vm,User.Identity.GetUserId(), this);

                    return Json(new { msg = "success_edit_review", partial = PartialReview });

                }
                else
                    return Json(new { msg = "success_add_review" });
            }
        }

        [HttpGet]
        public ActionResult EditReview(int id,int?page)
        {
            AddReviewViewModel vm = DataService.Get_EditReview(User.Identity.GetUserId(), id, page);

           return PartialView("_PartialAddReview", vm);
        }
    
        [HttpGet]
        public ActionResult DeleteReview(int id, int? page)
        {
            DeleteReviewViewModel vm = DataService.Get_DeleteReview(User.Identity.GetUserId(),id,page);
            return PartialView("_PartialDeleteReview",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(DeleteReviewViewModel vm)
        {
            IPagedList paged_list_reviews = DataService.Post_DeleteReview(vm, User.Identity.GetUserId());

            return PartialView("_PartialYourReviewList", paged_list_reviews);
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
                string[] result = DataService.Post_AddToFavorite(User.Identity.GetUserId(), this, hotel_id, city_id, page, sort);
             
                return Json(new { msg = result[0], partial = result[1] });
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
                string result = DataService.Post_AddToFavorite_Detail(User.Identity.GetUserId(), hotel_id);

                return Json(new { msg = result });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFavorite(int hotel_id, int? page)
        {
            IPagedList paged_list_wishlist =DataService.Post_DeleteFavorite(User.Identity.GetUserId(), hotel_id, page);

            return PartialView("_PartialWishList",paged_list_wishlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateHotel(int hotel_id, int your_rating, int? page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { msg = "login_required" });
            }

            DataService.Post_RateHotel(User.Identity.GetUserId(),your_rating,hotel_id);

            if (Request.IsAjaxRequest())
            {
                string partialview_rating = DataService.Get_PartialRatingList(this, User.Identity.GetUserId(), page);
                return Json(new { msg = "rating_success", partial = partialview_rating });
            }
            else
                return Json(new { msg = "rating_success" });
        }

        


    }
}