using System.Web;
using System.Web.Optimization;

namespace HotelAdvice
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        , "~/Scripts/jquery.unobtrusive-ajax.js"
                      ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/HomePage").Include(
                      "~/Scripts/respond.js"
                      , "~/Scripts/jquery-ui-1.12.0.min.js"
                      , "~/Scripts/Carousel.js"
                       , "~/Scripts/app_script/homePage.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/StarRating").Include(
                      "~/Scripts/StarRating/star-rating.min.js"
                      , "~/Scripts/StarRating/theme.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/AdminPage").Include(
                      "~/Scripts/app_script/admin.js"
                       , "~/Scripts/bootstrap-timepicker.min.js"
                       , "~/Scripts/fileinput.min.js"
                       , "~/Scripts/bootstrap-tagsinput.js"
                       , "~/Scripts/typeahead.js"
                       , "~/Scripts/bootstrap-sortable.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/Site.css"
                      , "~/Content/font-awesome.min.css"
                      , "~/Content/animate.css"
                      , "~/Content/bootstrap-social.css"
                      , "~/Content/themes/base/jquery-ui.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/AdminCss").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/bootstrap-theme.min.css", 
                     "~/Content/sidebar.css"
                     , "~/Content/font-awesome.min.css"
                     , "~/Content/AdminStyle.css"
                     , "~/Content/PagedList.css"
                    , "~/Content/bootstrap-timepicker.min.css"
                    , "~/Content/fileinput.min.css"
                    , "~/Content/bootstrap-tagsinput.css"
                    , "~/Content/typeahead_style.css"
                    , "~/Content/bootstrap-sortable.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/StarRating").Include(
                     "~/Content/StarRating/star-rating.min.css"
                    , "~/Content/StarRating/theme.min.css"
                    ));
        }
    }
}
