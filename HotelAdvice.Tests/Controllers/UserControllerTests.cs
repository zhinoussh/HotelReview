using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Areas.WebSite.Controllers;
using HotelAdvice.Helpers;
using System.Web.Mvc;
using HotelAdvice.Areas.WebSite.ViewModels;
using HotelAdvice.Areas.Admin.ViewModels;
using System.Collections.Generic;
using PagedList;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace HotelAdvice.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_NotAjaxRequest_Return_View()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);
            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_UserProfilePage(mockIdentity.Object.GetUserId(),1))
                    .Returns(new UserPageViewModel());

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (ViewResult)ctrl.Index(1, "");
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(UserPageViewModel),model.GetType());

        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_wishlist_Return_PartialView_with_lst_wishList()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_UserProfilePage(mockIdentity.Object.GetUserId(),1))
                .Returns(new UserPageViewModel()
            {
                 lst_wishList= new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
            });

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.Index(1, "wishlist");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialWishList", result.ViewName);
        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_ratinglist_Return_PartialView_with_lst_rating()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_UserProfilePage(mockIdentity.Object.GetUserId(), 1))
                .Returns(new UserPageViewModel()
                {
                    lst_rating = new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
                });

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.Index(1, "rating");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialRatingList", result.ViewName);
        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_reviewlist_Return_PartialView_with_lst_reviews()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_UserProfilePage(mockIdentity.Object.GetUserId(), 1))
                .Returns(new UserPageViewModel()
                {
                    lst_reviews = new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
                });

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.Index(1, "review");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialYourReviewList", result.ViewName);
        }
                
        [TestMethod]
        public void Index_AjaxRequest_Tab_is_NotDefined_Return_PartialView_with_lst_wishList()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_UserProfilePage(mockIdentity.Object.GetUserId(), 1))
                .Returns(new UserPageViewModel()
                {
                    lst_wishList = new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
                });

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.Index(1, "");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialWishList", result.ViewName);
        }

        [TestMethod]
        public void Get_Reviews_ForaHotel_NotAjaxRequest_Return_View() {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);
            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_ReviewPage(mockIdentity.Object.GetUserId(),ctrl,8,1))
                    .Returns(new ReviewPageViewModel());

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (ViewResult)ctrl.Reviews(8, 1);
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ReviewPageViewModel), model.GetType());
        }

        [TestMethod]
        public void Get_Reviews_ForaHotel_IsAjaxRequest_Return_PartialView_with_lst_reviews()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);
            var mockIdentity = new Mock<IIdentity>();

            service.Setup(x => x.Get_ReviewPage(mockIdentity.Object.GetUserId(), ctrl, 8, 1))
                    .Returns(new ReviewPageViewModel()
                    {
                        lst_reviews = new List<ReviewListViewModel>() { 
                            new ReviewListViewModel(),
                            new ReviewListViewModel()
                        }.ToPagedList(1,10)
                    });

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.Reviews(8, 1);
            IPagedList<ReviewListViewModel> model = (IPagedList<ReviewListViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialHotelReviewList",result.ViewName);
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void Post_AddReview_For_NonAuthenticatedUser_Return_JsonResult_ToLogin()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);
          
            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, false);
            var result=(JsonResult)ctrl.Add_Review(new AddReviewViewModel());
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("login_required", Json_msg);

        }

        [TestMethod]
        public void Post_AddReview_For_AuthenticatedUser_fromProfilePage_Return_JsonResult_ToReplacePartialView()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity=new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            AddReviewViewModel review=new AddReviewViewModel()
            {
                fromProfilePage = true
            };

            service.Setup(x => x.Post_AddNewReview(review)).Verifiable();

            string partial = "";
            service.Setup(x => x.Get_PartialReviewList(review, mockIdentity.Object.GetUserId(), ctrl))
                    .Returns(partial);
           
            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, true);

            var result = (JsonResult)ctrl.Add_Review(review);
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");
            string Json_partial= JsonDecoder.GetValueFromJsonResult<string>(result, "partial");

            //Assert
            service.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("success_edit_review", Json_msg);
            Assert.AreEqual(partial, Json_partial);

        }

        [TestMethod]
        public void Post_AddReview_For_AuthenticatedUser_fromReviewPage_Return_JsonResult()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            AddReviewViewModel review = new AddReviewViewModel()
            {
                fromProfilePage = false
            };

            service.Setup(x => x.Post_AddNewReview(review)).Verifiable();

            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, true);

            var result = (JsonResult)ctrl.Add_Review(review);
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            service.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("success_add_review", Json_msg);

        }

        [TestMethod]
        public void Get_EditReview_Return_PartialView_AsEditModal()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            service.Setup(x => x.Get_EditReview(mockIdentity.Object.GetUserId(), 8,1))
                    .Returns(new AddReviewViewModel());

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.EditReview(8,1);
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialAddReview", result.ViewName);
            Assert.AreEqual(typeof(AddReviewViewModel), model.GetType());

        }

        [TestMethod]
        public void Get_DeleteReview_Return_PartialView_AsDeleteModal()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            service.Setup(x => x.Get_DeleteReview(mockIdentity.Object.GetUserId(), 8, 1))
                    .Returns(new DeleteReviewViewModel() { 
                        hotelId=8
                    });

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.DeleteReview(8, 1);
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialDeleteReview", result.ViewName);
            Assert.AreEqual(typeof(DeleteReviewViewModel), model.GetType());

        }

        [TestMethod]
        public void Post_DeleteReview_Return_PartialView_InProfilePage_with_ReviewList()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            DeleteReviewViewModel review_to_delete = new DeleteReviewViewModel() { hotelId=8};
            service.Setup(x => x.Post_DeleteReview(review_to_delete, mockIdentity.Object.GetUserId()))
                    .Returns(new List<HotelSearchViewModel>()
                    {
                        new HotelSearchViewModel(),
                        new HotelSearchViewModel()
                    }.ToPagedList(1, 10));

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.DeleteReview(review_to_delete);
            IPagedList<HotelSearchViewModel> model = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialYourReviewList", result.ViewName);
            Assert.AreEqual(2, model.Count);

        }

        [TestMethod]
        public void Post_AddToFavorite_For_NonAuthenticatedUser_Return_JsonResult_ToLogin()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, false);
            var result = (JsonResult)ctrl.AddToFavorite(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(),It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<string>());

            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("login_required", Json_msg);

        }

        [TestMethod]
        public void Post_AddToFavorite_For_AuthenticatedUser_Return_JsonResult_ToReplacePartialView()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            string[] result_addFavorite = new string[2];

            service.Setup(x => x.Post_AddToFavorite(mockIdentity.Object.GetUserId(), ctrl, It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(result_addFavorite);
            
            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, true);

            var result = (JsonResult)ctrl.AddToFavorite(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<string>());

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Post_AddToFavorite_Detail_For_NonAuthenticatedUser_Return_JsonResult_ToLogin()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, false);
            var result = (JsonResult)ctrl.AddToFavorite_Detail(It.IsAny<int>());

            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("login_required", Json_msg);

        }

        [TestMethod]
        public void Post_AddToFavorite_Detail_For_AuthenticatedUser_Return_JsonResult()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            string result_addFavorite = "";

            service.Setup(x => x.Post_AddToFavorite_Detail(mockIdentity.Object.GetUserId(),It.IsAny<int>()))
                .Returns(result_addFavorite);

            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, true);

            var result = (JsonResult)ctrl.AddToFavorite_Detail(It.IsAny<int>());
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result_addFavorite,Json_msg);

        }

        [TestMethod]
        public void Post_DeleteFavorite_Return_PartialView_InProfilePage_with_wishlist()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);

            service.Setup(x => x.Post_DeleteFavorite(mockIdentity.Object.GetUserId(),8,1))
                    .Returns(new List<HotelSearchViewModel>()
                    {
                        new HotelSearchViewModel(),
                        new HotelSearchViewModel()
                    }.ToPagedList(1, 10));

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result = (PartialViewResult)ctrl.DeleteFavorite(8,1);
            IPagedList<HotelSearchViewModel> model = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialWishList", result.ViewName);
            Assert.AreEqual(2, model.Count);

        }

        [TestMethod]
        public void Post_RateHotel_For_NonAuthenticatedUser_Return_JsonResult_ToLogin()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            UserController ctrl = new UserController(service.Object);

            //Act
            MakeRequest.MakeAuthenticationRequest(ctrl, false);
            var result = (JsonResult)ctrl.RateHotel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("login_required", Json_msg);

        }

        [TestMethod]
        public void Post_RateHotel_For_AuthenticatedUser_IsAjaxRequest_Return_JsonResult_ToReplacePartialView()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);


            service.Setup(x => x.Post_RateHotel(mockIdentity.Object.GetUserId(), It.IsAny<int>(), It.IsAny<int>()))
                   .Verifiable();

            string result_rate = "";

            service.Setup(x => x.Get_PartialRatingList(ctrl, mockIdentity.Object.GetUserId(), It.IsAny<int>()))
                 .Returns(result_rate);
            //Act
            MakeRequest.MakeAjaxRequest(ctrl, true);

            var result = (JsonResult)ctrl.RateHotel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");
            string Json_partial = JsonDecoder.GetValueFromJsonResult<string>(result, "partial");

            //Assert
            service.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("rating_success", Json_msg);
            Assert.IsNotNull(Json_partial);
            Assert.AreEqual(result_rate, Json_partial);

        }

        [TestMethod]
        public void Post_RateHotel_For_AuthenticatedUser_NotAjaxRequest_Return_JsonResult()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            var mockIdentity = new Mock<IIdentity>();
            UserController ctrl = new UserController(service.Object);


            service.Setup(x => x.Post_RateHotel(mockIdentity.Object.GetUserId(), It.IsAny<int>(), It.IsAny<int>()))
                   .Verifiable();
            //Act
            MakeRequest.MakeNormalRequest(ctrl, true);

            var result = (JsonResult)ctrl.RateHotel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            string Json_msg = JsonDecoder.GetValueFromJsonResult<string>(result, "msg");

            //Assert
            service.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("rating_success", Json_msg);

        }

    }
}
