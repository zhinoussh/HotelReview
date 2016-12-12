using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelAdvice.Areas.Admin.Controllers;
using System.Web.Mvc;
using Moq;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Routing;

namespace HotelAdvice.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Index_Return_IndexView()
        {
            //Arrange
            
            AdminController ctrl = new AdminController();
            
            //Act
            var result = (ViewResult)ctrl.Index();

            //Assert
            Assert.IsNotNull(result);
        }

       
    }
}
