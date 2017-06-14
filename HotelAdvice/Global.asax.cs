using HotelAdvice.App_Code;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HotelAdvice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<HotelAdviceDB>(new HotelAdviceDBInitializer());
            //Database.SetInitializer<ApplicationDbContext>(new IdentityDBInitializer());

            //Database.SetInitializer<HotelAdviceDB>(new HotelAdviceDBInitializer());
            //HotelAdviceDB context = new HotelAdviceDB();
            //context.Database.Initialize(false);

            //Database.SetInitializer<ApplicationDbContext>(new IdentityDBInitializer());
            //ApplicationDbContext context2 = new ApplicationDbContext();
            //context2.Database.Initialize(false);

         //   UnityWebActivator.Start();
          //  Bootstrapper.Initialise();

            HotelDataBaseInitializer.Initialize();

        }

        
    }


}
