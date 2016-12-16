using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Areas.Admin.Controllers;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using System.Collections.Generic;
using HotelAdvice.Helpers;
using System.Web.Mvc;

namespace HotelAdvice.Tests.Controllers
{
    [TestClass]
    public class AmenityControllerTests
    {
        [TestMethod]
        public void Index_NotAjaxRequest_ReturnView_With_AmenityList()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_AmenityList(1, "amenityFilter")).Returns(new List<AmenityViewModel>()
            {
                new AmenityViewModel(){},
                new AmenityViewModel(){},
                new AmenityViewModel(){},
            }.ToPagedList(1, 10));

            AmenityController ctrl = new AmenityController(repo.Object);

            //Act
            MakeRequest.MakeNormalRequest(ctrl,false);
            var result=(ViewResult)ctrl.Index(1, "amenityFilter");
            IPagedList<AmenityViewModel> model = (IPagedList<AmenityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, model.Count);
            Assert.AreEqual("amenityFilter", result.ViewData["filter"]);
        }

        [TestMethod]
        public void Index_AjaxRequest_Return_PartialView_With_AmenityList() { 
            
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_AmenityList(1, "myFilter")).Returns(new List<AmenityViewModel>()
            {
                new AmenityViewModel(){},
                new AmenityViewModel(){},
                new AmenityViewModel(){}
            }.ToPagedList(1, 10));

            AmenityController ctrl = new AmenityController(repo.Object);

            //Act
            MakeRequest.MakeAjaxRequest(ctrl,null);
            var result=(PartialViewResult)ctrl.Index(1, "myFilter");
            IPagedList<AmenityViewModel> model = (IPagedList<AmenityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, model.Count);
            Assert.AreEqual("myFilter", result.ViewData["filter"]);
            Assert.AreEqual("_PartialAmenityList", result.ViewName);

        }

        [TestMethod]
        public void Post_AddNewAmenity_Retunr_Josn() { 
            
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Post_AddNewAmenity("amenity", "1", 1)).Verifiable();

            AmenityController ctrl = new AmenityController(repo.Object);

            //Act
            var result=(JsonResult)ctrl.ADD_New_Amenity("amenity", "1", 1);
            string Json_ctrl = JsonDecoder.GetValueFromJsonResult<string>(result, "ctrl");
            string Json_cur_pg = JsonDecoder.GetValueFromJsonResult<string>(result, "cur_pg");

            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual("/Admin/Amenity", Json_ctrl);
            Assert.AreEqual("1", Json_cur_pg);
        }

        [TestMethod]
        public void Get_DeleteAmenity_Retunr_PartialView_As_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_DeleteAmenity(1, 1, "")).Returns(new AmenityViewModel()
            {
                AmenityID=1
            });
            AmenityController ctrl = new AmenityController(repo.Object);

            //Act
            var result = (PartialViewResult)ctrl.Delete_Amenity(1, 1, "");
            AmenityViewModel model = (AmenityViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, model.AmenityID);
            Assert.AreEqual("_PartialDeleteAmenity", result.ViewName);
        }

        [TestMethod]
        public void Get_DeleteAmenity_Retunr_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            AmenityViewModel vm = new AmenityViewModel()
            {
                AmenityID = 1,
                CurrentFilter = "filter",
                CurrentPage = 1
            };

            repo.Setup(x => x.Post_DeleteAmenity(vm)).Verifiable();
            
            AmenityController ctrl = new AmenityController(repo.Object);

            //Act
            var result = (JsonResult)ctrl.Delete_Amenity(vm);
            int Json_cur_pg = JsonDecoder.GetValueFromJsonResult<int>(result, "cur_pg");
            string Json_filter = JsonDecoder.GetValueFromJsonResult<string>(result, "filter");
            string Json_ctrl = JsonDecoder.GetValueFromJsonResult<string>(result, "ctrl");

            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, Json_cur_pg);
            Assert.AreEqual("filter", Json_filter);
            Assert.AreEqual("/Admin/Amenity", Json_ctrl);
        }


    }
}
