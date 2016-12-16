using System.IO;
using System.Web.Mvc;

namespace HotelAdvice.Helper
{
    //public interface IRenderPartialView
    //{
    //    static string RenderRazorViewToString(Controller controller, string viewName, object model);
    //}

    public class RenderPartialView //: IRenderPartialView
    {

        public static string RenderRazorViewToString(Controller controller,string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View,
                                             controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
       
    }
}