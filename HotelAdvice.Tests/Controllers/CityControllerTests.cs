using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelAdvice.Areas.Admin.Controllers;
using Moq;
using HotelAdvice.DataAccessLayer;
using PagedList;
using HotelAdvice.Areas.Admin.ViewModels;
using System.Web.Mvc;
using HotelAdvice.Helpers;

namespace HotelAdvice.Controllers
{
    /// <summary>
    /// Summary description for CityControllerTests
    /// </summary>
    [TestClass]
    public class CityControllerTests
    {
        
        [TestMethod]
        public void Index_NotAjaxRequests_ReturnView_With_CityList()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            repo.Setup(x => x.Get_CityList(1, ""))
               .Returns((new List<CityViewModel>(){
                new CityViewModel{},
                new CityViewModel{},
                new CityViewModel{}
                }).ToPagedList(1, 10)
               );

            CityController ctrl = new CityController(repo.Object);
            
            //Act
            ctrl.MakeNormalRequest();    
            
            var result = (ViewResult)ctrl.Index(1, "");
            IPagedList<CityViewModel> model = (IPagedList<CityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
          //  Assert.AreEqual("Myfilter", result.ViewData["filter"]);
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public void Index_AjaxRequests_ReturnPartialView_With_CityList()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            repo.Setup(x => x.Get_CityList(1, ""))
                .Returns((new List<CityViewModel>(){
                new CityViewModel{},
                new CityViewModel{},
                new CityViewModel{}
                }).ToPagedList(1, 10)
                );

            CityController ctrl = new CityController(repo.Object);

            //Act
            ctrl.MakeAjaxRequest();

            var result = (PartialViewResult)ctrl.Index(1, "");
            IPagedList<CityViewModel> model = (IPagedList<CityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialCityList", result.ViewName);
            Assert.AreEqual(3, model.Count);
        }

        [TestMethod]
        public void Get_ADDNewCity_Return_PartialView_AS_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            CityController ctrl = new CityController(repo.Object);

            repo.Setup(x => x.Get_AddNewCity(ctrl,1, 1, ""))
                .Returns(new CityViewModel
                {
                    cityID = 1,
                    cityName = ""
                });

           
            //Act
            var result = (PartialViewResult)ctrl.ADD_New_City(1, 1, "");
            CityViewModel model =(CityViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialAddCity", result.ViewName);
            Assert.AreEqual(1, model.cityID);
        }

        [TestMethod]
        public void Post_ADDNewCity_Return_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            CityViewModel new_city = new CityViewModel
            {
                cityID = 1,
                cityName = "",
                CurrentFilter = "Myfilter",
                CurrentPage = 1
            };
            CityController ctrl = new CityController(repo.Object);

            repo.Setup(x => x.Post_AddNewCity(new_city, ctrl)).Verifiable();


            //Act
            var result = (JsonResult)ctrl.ADD_New_City(new_city);

            int Json_cur_pg = JsonDecoder.GetValueFromJsonResult<int>(result, "cur_pg");
            string Json_filter = JsonDecoder.GetValueFromJsonResult<string>(result, "filter");
            string Json_ctrl = JsonDecoder.GetValueFromJsonResult<string>(result, "ctrl");

            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, Json_cur_pg);
            Assert.AreEqual("Myfilter", Json_filter);
            Assert.AreEqual("/Admin/City", Json_ctrl);
           
        }

        [TestMethod]
        public void Get_CityDescription_Return_PartialView_As_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            repo.Setup(x => x.GetCityDescription(1)).Returns(new CityViewModel
            {
                cityID=1,
                cityAttractions="",
                cityName=""
            });
               
            CityController ctrl = new CityController(repo.Object);

            //Act
            var result = (PartialViewResult)ctrl.CityDescription(1);
            CityViewModel model = (CityViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialDescription", result.ViewName);
            Assert.AreEqual(1, model.cityID);
        }

        [TestMethod]
        public void Get_DeleteCity_Return_PartialView_AS_Modal()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);

            repo.Setup(x => x.Get_DeleteCity(1,1,"")).Returns(new CityViewModel
            {
                cityID = 1,
                cityAttractions = "",
                cityName = ""
            });

            CityController ctrl = new CityController(repo.Object);

            //Act
            var result = (PartialViewResult)ctrl.Delete_City(1,1,"");
            CityViewModel model = (CityViewModel)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("_PartialDeleteCity", result.ViewName);
            Assert.AreEqual(1, model.cityID);
        }

        [TestMethod]
        public void Post_DeleteCity_Return_Json()
        {
            //Arrange
            var repo = new Mock<IServiceLayer>(MockBehavior.Strict);
            CityController ctrl = new CityController(repo.Object);

            CityViewModel city_to_delete = new CityViewModel
            {
                cityID = 1,
                cityAttractions = "",
                cityName = "",
                CurrentFilter="MyFilter",
                CurrentPage=1
            };
            repo.Setup(x => x.Post_DeleteCity(ctrl,city_to_delete)).Verifiable();


            //Act
            var result = (JsonResult)ctrl.Delete_City(city_to_delete);
            int Json_cur_pg = JsonDecoder.GetValueFromJsonResult<int>(result, "cur_pg");
            string Json_filter = JsonDecoder.GetValueFromJsonResult<string>(result, "filter");
            string Json_ctrl = JsonDecoder.GetValueFromJsonResult<string>(result, "ctrl");
            
            //Assert
            repo.Verify();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, Json_cur_pg);
            Assert.AreEqual("MyFilter", Json_filter);
            Assert.AreEqual("/Admin/City", Json_ctrl);
        }

    }
}
