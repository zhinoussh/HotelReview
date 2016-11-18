using System.Web;
using System.Web.Mvc;

namespace HotelAdvice.Areas.Admin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}