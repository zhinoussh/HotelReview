using System;
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

namespace HotelAdvice.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_NotAjaxRequest_Return_View()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.Get_HomePage(1)).Returns(new HomeViewModel());
            HomeController ctrl = new HomeController(service.Object);

            //Act
            MakeRequest.MakeNormalRequest(ctrl);
            var result=(ViewResult)ctrl.Index("returnUrl", 1, "");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("returnUrl", result.ViewData["ReturnUrl"]);

        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_citylist_Return_PartialView_with_lst_city()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.Get_HomePage(1)).Returns(new HomeViewModel()
            {
                lst_city = new List<CityViewModel>() { 
                    new CityViewModel(),
                    new CityViewModel()
                }.ToPagedList(1,10)
            });
            HomeController ctrl = new HomeController(service.Object);

            //Act
            MakeRequest.MakeAjaxRequest(ctrl);
            var result = (PartialViewResult)ctrl.Index("returnUrl", 1, "citylist");
            IPagedList<CityViewModel> vm = (IPagedList<CityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,vm.Count);
            Assert.AreEqual("_PartialCityList", result.ViewName);
            Assert.AreEqual("returnUrl", result.ViewData["ReturnUrl"]);
        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_popularlist_Return_PartialView_with_lst_poupular_hotels()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.Get_HomePage(1)).Returns(new HomeViewModel()
            {
                lst_poupular_hotels = new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
            });
            HomeController ctrl = new HomeController(service.Object);

            //Act
            MakeRequest.MakeAjaxRequest(ctrl);
            var result = (PartialViewResult)ctrl.Index("returnUrl", 1, "popularlist");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialPopularHotels", result.ViewName);
            Assert.AreEqual("returnUrl", result.ViewData["ReturnUrl"]);
        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_toplist_Return_PartialView_with_lst_top_hotels()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.Get_HomePage(1)).Returns(new HomeViewModel()
            {
                lst_top_hotels = new List<HotelSearchViewModel>() { 
                    new HotelSearchViewModel(),
                    new HotelSearchViewModel()
                }.ToPagedList(1, 10)
            });
            HomeController ctrl = new HomeController(service.Object);

            //Act
            MakeRequest.MakeAjaxRequest(ctrl);
            var result = (PartialViewResult)ctrl.Index("returnUrl", 1, "toplist");
            IPagedList<HotelSearchViewModel> vm = (IPagedList<HotelSearchViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialTopHotels", result.ViewName);
            Assert.AreEqual("returnUrl", result.ViewData["ReturnUrl"]);
        }

        [TestMethod]
        public void Index_AjaxRequest_Tab_is_NotDefined_Return_PartialView_with_lst_city()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.Get_HomePage(1)).Returns(new HomeViewModel()
            {
                lst_city = new List<CityViewModel>() { 
                    new CityViewModel(),
                    new CityViewModel()
                }.ToPagedList(1, 10)
            });
            HomeController ctrl = new HomeController(service.Object);

            //Act
            MakeRequest.MakeAjaxRequest(ctrl);
            var result = (PartialViewResult)ctrl.Index("returnUrl", 1, "");
            IPagedList<CityViewModel> vm = (IPagedList<CityViewModel>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, vm.Count);
            Assert.AreEqual("_PartialCityList", result.ViewName);
            Assert.AreEqual("returnUrl", result.ViewData["ReturnUrl"]);
        }

        [TestMethod]
        public void SearchList_Return_filteredList_withPrefix()
        {
            //Arrane
            var service = new Mock<IServiceLayer>(MockBehavior.Strict);
            service.Setup(x => x.search_destinations_by_prefix("pre"))
                .Returns(new List<string>() { 
                    "pre1",
                    "pre2"
                });
            HomeController ctrl = new HomeController(service.Object);

            //Act
            var result = (JsonResult)ctrl.SearchList("pre");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Data.GetType().GetProperty("Count").GetValue(result.Data));

        }
       
    }
}
