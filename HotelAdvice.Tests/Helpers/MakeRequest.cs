using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelAdvice.Helpers
{
    public static class MakeRequest
    {
        public static void MakeAjaxRequest(this Controller controller)
        {
            // First create request with X-Requested-With header set
            Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
            httpRequest.SetupGet(x => x.Headers).Returns(
                new WebHeaderCollection() {
            {"X-Requested-With", "XMLHttpRequest"}
            }
            );

            // Then create contextBase using above request
            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object);

            // Set controllerContext
            controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
        }

        public static void MakeNormalRequest(this Controller controller)
        {
            // First create request with X-Requested-With header set
            Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
            

            // Then create contextBase using above request
            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object);


            httpRequest.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "NotAjaxRequest" } });
            httpRequest.SetupGet(x => x["X-Requested-With"]).Returns("NotAjaxRequest");

            // Set controllerContext
            controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
        }
    }
}
