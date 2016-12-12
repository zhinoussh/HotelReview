using Microsoft.Owin.Security;
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

       
    }
}