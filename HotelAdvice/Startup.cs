using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelAdvice.Startup))]
namespace HotelAdvice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
