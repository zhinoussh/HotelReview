using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Areas.Admin.ViewModels;
using System.Collections.Generic;
using PagedList;
using HotelAdvice.Areas.Admin.Controllers;
using HotelAdvice.Helpers;
using System.Web.Mvc;
using HotelAdvice.Areas.Admin.Models;

namespace HotelAdvice.Controllers
{
    [TestClass]
    public class HotelControllerTests
    {
        [TestMethod]
        public void Index_NotAjaxRequests_ReturnView_With_HotelList()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_HotelList(1, "filter")).Returns(new List<HotelViewModel>{
                new HotelViewModel(){},
                new HotelViewModel(){},
                new HotelViewModel(){}
            }.ToPagedList<HotelViewModel>(1,10));

            HotelController ctrl = new HotelController(repo.Object);
            
            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result=(ViewResult)ctrl.Index(1, "filter");
            IPagedList<HotelViewModel> model=(IPagedList<HotelViewModel>)result.Model;
           
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, model.Count);
            Assert.AreEqual("filter", result.ViewData["filter"]);

        }

        [TestMethod]
        public void Index_AjaxRequests_ReturnPartialView_With_HotelList()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_HotelList(1, "")).Returns(new List<HotelViewModel>{
                new HotelViewModel(){},
                new HotelViewModel(){},
                new HotelViewModel(){}
                }.ToPagedList(1,10)
                );

            HotelController ctrl = new HotelController(repo.Object);

            //act
            MakeRequest.MakeAjaxRequest(ctrl,false);
            var result=(PartialViewResult)ctrl.Index(1, "");
            IPagedList<HotelViewModel> model = (IPagedList<HotelViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialHotelList", result.ViewName);
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public void Get_ADDNewHotel_Return_PartialView_AS_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            HotelController ctrl=new HotelController(repo.Object);
            repo.Setup(x => x.Get_AddNewHotel(ctrl, 1, 1, ""))
                .Returns(new HotelViewModel() { 
                    HotelId=1,
                    HotelName=""
                });

            //Act
            var result=(PartialViewResult)ctrl.ADD_New_Hotel(1,1,"");
            HotelViewModel vm = (HotelViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialAddHotel", result.ViewName);
            Assert.AreEqual(1, vm.HotelId);
        }

        [TestMethod]
        public void Post_ADDNewHotel_Return_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            HotelViewModel hotel=new HotelViewModel(){
                HotelId=1,
                HotelName="",
                CurrentFilter="Hotelfilter",
                CurrentPage=1
            };
            HotelController ctrl=new HotelController(repo.Object);

            repo.Setup(x => x.Post_AddNewHotel(hotel, ctrl)).Verifiable();

            //Act
            var result=(JsonResult)ctrl.ADD_New_Hotel(hotel);
            string Json_ctrl= JsonDecoder.GetValueFromJsonResult<string>(result,"ctrl");
            string Json_filter= JsonDecoder.GetValueFromJsonResult<string>(result,"filter");
            int Json_cur_pg= JsonDecoder.GetValueFromJsonResult<int>(result,"cur_pg");
            
            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("/Admin/Hotel", Json_ctrl);
            Assert.AreEqual("Hotelfilter",Json_filter);
            Assert.AreEqual(1, Json_cur_pg);
        }

        [TestMethod]
        public void Get_HotelDescription_Return_PartialView_As_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.GetHotelDescription(1)).Returns(new HotelViewModel()
            {
                HotelId = 1,
                Description = ""
            });

            HotelController ctrl = new HotelController(repo.Object);

            //Act
            var result=(PartialViewResult)ctrl.HotelDescription(1);
            HotelViewModel vm = (HotelViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialDescription", result.ViewName);
            Assert.AreEqual(1, vm.HotelId);

        }

        [TestMethod]
        public void Get_DeleteHotel_Return_PartialView_As_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_DeleteHotel(1,1,"")).Returns(new HotelViewModel()
            {
                HotelId = 1,
                HotelName=""
            });

            HotelController ctrl = new HotelController(repo.Object);
            //HotelViewModel hotel=new HotelViewModel(){
            //    HotelId=1
            //};

            //Act
            var result=(PartialViewResult)ctrl.Delete_Hotel(1, 1, "");
            HotelViewModel model = (HotelViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialDeleteHotel", result.ViewName);
            Assert.AreEqual(1,model.HotelId);

        }

        [TestMethod]
        public void Post_DeleteHotel_Return_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            HotelController ctrl = new HotelController(repo.Object);
            HotelViewModel hotel=new HotelViewModel(){
                HotelId=1,
                CurrentPage=1,
                CurrentFilter="hotelFilter"
            };

            repo.Setup(x => x.Post_DeleteHotel(hotel, ctrl)).Verifiable();

            //Act
            var result=(JsonResult)ctrl.Delete_Hotel(hotel);
            string Json_ctrl = JsonDecoder.GetValueFromJsonResult<string>(result, "ctrl");
            string Json_filter = JsonDecoder.GetValueFromJsonResult<string>(result, "filter");
            int Json_cur_pg = JsonDecoder.GetValueFromJsonResult<int>(result, "cur_pg");

            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("/Admin/Hotel", Json_ctrl);
            Assert.AreEqual("hotelFilter", Json_filter);
            Assert.AreEqual(1, Json_cur_pg);

        }

        [TestMethod]
        public void Get_HotelImages_Return_View()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            HotelController ctrl = new HotelController(repo.Object);

            repo.Setup(x => x.Get_HotelImagesView(1, ctrl))
                .Returns(new HotelImagesViewModel()
                {
                    HotelId=1
                });

            //Act
            var result=(ViewResult)ctrl.HotelImages(1);
            var model=(HotelImagesViewModel)result.Model;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.HotelId);
        }

        [TestMethod]
        public void Post_AddImage_Return_Josn()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            HotelController ctrl = new HotelController(repo.Object);
            HotelImagesViewModel images = new HotelImagesViewModel()
            {
              HotelId=1
            };

            repo.Setup(x => x.Post_AddHotelPhoto(images,1, ctrl)).Verifiable();

            //Act
            var result=(JsonResult)ctrl.AddImage(images, 1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_DeleteHotelPhoto_Return_PartialView_As_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            HotelController ctrl = new HotelController(repo.Object);

            repo.Setup(x => x.Get_DeleteHotelPhoto("photo"))
                .Returns(new HotelImagesViewModel() {
                    PhotoName = "photo"
                    ,HotelId=1
                });

            //Act
            var result = (PartialViewResult)ctrl.Delete_HotelPhoto("photo");
            HotelImagesViewModel model = (HotelImagesViewModel)result.Model;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("photo", model.PhotoName);
            Assert.AreEqual("_PartialDeletePhoto", result.ViewName);
        }

        [TestMethod]
        public void Post_DeleteHotelPhoto_Return_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            HotelController ctrl = new HotelController(repo.Object);
            HotelImagesViewModel image = new HotelImagesViewModel();

            repo.Setup(x => x.Post_DeleteHotelPhoto(image,ctrl)).Verifiable();

            //Act
            var result = (JsonResult)ctrl.Delete_HotelPhoto(image);
            
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_ShowHotelPhoto_Return_PartialView_As_Modal()
        { 
            //Arrange
            var repo=new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x=>x.Get_HotelPhoto("photo"))
                .Returns(new HotelImagesViewModel(){
                    PhotoName="photo"
                });

            HotelController ctrl=new HotelController(repo.Object);

            //Act
            var result=(PartialViewResult)ctrl.Show_HotelPhoto("photo");
             HotelImagesViewModel model=(HotelImagesViewModel)result.Model;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialShowImage", result.ViewName);
            Assert.AreEqual("photo", model.PhotoName);
        }

        [TestMethod]
        public void Get_Restaurants_ReturnJson()
        { 
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x=>x.DataLayer.get_restaurants())
                .Returns(new List<string>(){
                    "res1",
                   "res2",
                   "sssss"
                });

            HotelController ctrl = new HotelController(repo.Object);

            //Act
            var result=(JsonResult)ctrl.Get_Restaurants("res");
            var countPropertyInfo = result.Data.GetType().GetProperty("Count");
            int expected_count=(int)countPropertyInfo.GetValue(result.Data);
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,expected_count);

        }

        [TestMethod]
        public void Get_Rooms_ReturnJson()
        {
            //arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.DataLayer.get_roomTypes())
                .Returns(new List<string>()
                {
                    "pre1",
                    "pre2",
                    "lllll",
                }
            );

            HotelController ctrl = new HotelController(repo.Object);

            //act
            var result=(JsonResult)ctrl.Get_Rooms("pre");
            var countPropertyInfo=result.Data.GetType().GetProperty("Count");
            var expected_count=countPropertyInfo.GetValue(result.Data);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, expected_count);


        }

        [TestMethod]
        public void Get_Sightseeing_ReturnJson()
        {
            //arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.DataLayer.get_Sightseeing())
                .Returns(new List<string>()
                {
                    "sight1",
                     "sight2",
                     "llll",
                });

            HotelController ctrl = new HotelController(repo.Object);

            //act
            var result=(JsonResult)ctrl.Get_SightSeeing("sight"); ;
            var countPropertyInfo = result.Data.GetType().GetProperty("Count");
            var expected_count=countPropertyInfo.GetValue(result.Data);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, expected_count);
        }
    }
}
