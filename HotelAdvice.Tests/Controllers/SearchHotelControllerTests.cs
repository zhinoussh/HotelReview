using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HotelAdvice.DataAccessLayer;
using HotelAdvice.Areas.WebSite.Controllers;
using HotelAdvice.Helpers;
using System.Web.Mvc;
using System.Collections.Generic;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace HotelAdvice.Tests.Controllers
{
    [TestClass]
    public class SearchHotelControllerTests
    {
        [TestMethod]
        public void ShowSearchResult_Ajaxrequest_Return_PartialSearchResults_As_JSON()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
                     
            var mockIdentity = new Mock<IIdentity>();

            SearchHotelController ctrl = new SearchHotelController(service.Object);

            service.Setup(x => x.Get_SearchResults_AsPartialView(mockIdentity.Object.GetUserId(),ctrl,1,"", "isfahan", false,"", 1, 1,1, "", false, false, false, false, false, ""))
                   .Returns(It.IsNotIn<string>());
         
            //Act
            MakeRequest.MakeAjaxRequest(ctrl);

            var result = (JsonResult)ctrl.ShowSearchResult(1, "", "isfahan", false, "", 1, 1, 1, "", false, false, false, false, false, "");
            string partial_Json=JsonDecoder.GetValueFromJsonResult<string>(result, "partial");

            //Assert
            Assert.IsNotNull(result);
           // Assert.AreEqual(partial_Json,);
        }

        [TestMethod]
        public void ShowSearchResult_NonAjaxrequest_Return_View()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
           
            var mockIdentity = new Mock<IIdentity>();
            service.Setup(x => x.Get_SearchResults_AsView(mockIdentity.Object.GetUserId(), "isfahan", false, "", 1, 1, 1, "", false, false, false, false, false, ""))
               .Returns(new SearchPageViewModel() {
               });
            SearchHotelController ctrl = new SearchHotelController(service.Object);

            //Act
            MakeRequest.MakeNormalRequest(ctrl);

            var result = (ViewResult)ctrl.ShowSearchResult(1, "", "isfahan", false, "", 1, 1, 1, "", false, false, false, false, false, "");
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(SearchPageViewModel),model.GetType());
        }

        [TestMethod]
        public void Advanced_Search_Return_Json()
        {
            //Arrange
            var service=new Mock<IServiceLayer>(MockBehavior.Strict);
            SearchHotelController ctrl = new SearchHotelController(service.Object);

            AdvancedSearchViewModel vm= new AdvancedSearchViewModel()
            {
                Hotel_Name="koroush",
                selected_city=8                
            };

            //ACT
            var result = (JsonResult)ctrl.Advanced_Search(vm);
            var searchcriteria = JsonDecoder.GetValueFromJsonResult<AdvancedSearchViewModel>(result, "searchcriteria");
            
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(searchcriteria, typeof(AdvancedSearchViewModel));
            Assert.AreEqual(searchcriteria.selected_city, vm.selected_city);
            Assert.AreEqual(searchcriteria.Hotel_Name, vm.Hotel_Name);

        }

        [TestMethod]
        public void GeneralSearch_Return_Json()
        {
            //Arrange
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            SearchHotelController ctrl = new SearchHotelController(service.Object);

            //ACT
            var result = (JsonResult)ctrl.GeneralSearch("Isfahan");
            string destination = JsonDecoder.GetValueFromJsonResult<string>(result, "destination_name");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(destination, typeof(string));
            Assert.AreEqual(destination, "Isfahan");

        }

        [TestMethod]
        public void HotelDetails_Return_View()
        {
            //Arrange
             var mockIdentity = new Mock<IIdentity>();

            var service = new Mock<IServiceLayer>(MockBehavior.Strict);

            service.Setup(x => x.Get_HotelDetails(mockIdentity.Object.GetUserId(), 8))
                    .Returns(new HotelDetailViewModel()
                    {
                        HotelId=8
                    });

            SearchHotelController ctrl = new SearchHotelController(service.Object);

            //ACT
            MakeRequest.MakeNormalRequest(ctrl);

            var result = (ViewResult)ctrl.HotelDetails(8);
            HotelDetailViewModel model = (HotelDetailViewModel)result.Model;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model.HotelId,8);
        }
    }
}
