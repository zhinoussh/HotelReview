using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace HotelAdvice
{

    public static class AjaxActionLink
    {
        public static MvcHtmlString SortLink(this AjaxHelper ajaxHelper, string linkText, string ActionName
        , string ControllerName, string sortField)
        {
            return SortLink(ajaxHelper, linkText, ActionName, ControllerName, sortField, "", "");
        }


        public static MvcHtmlString SortLink(this AjaxHelper ajaxHelper, string linkText, string ActionName
            , string ControllerName , string sortField,string cssClass,string title,object htmlAttributes=null)
        {
            RouteValueDictionary attr = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if (!String.IsNullOrEmpty(title))
                attr.Add("title", title);
           
            if(!String.IsNullOrEmpty(cssClass))
                attr.Add("class", cssClass);

            NameValueCollection QueryStringCollection=ajaxHelper.ViewContext.HttpContext.Request.QueryString;

            RouteValueDictionary routeValues=new RouteValueDictionary();
           // routeValues.Add("Area", "WebSite");
            routeValues.Add("sort", sortField);
            routeValues.Add("page", QueryStringCollection.Get("page"));
            routeValues.Add("cityId", QueryStringCollection.Get("cityId"));
            routeValues.Add("citySearch", QueryStringCollection.Get("citySearch"));
            routeValues.Add("destination_name", QueryStringCollection.Get("destination_name"));
            routeValues.Add("HotelName", QueryStringCollection.Get("HotelName"));
            routeValues.Add("center", QueryStringCollection.Get("center"));
            routeValues.Add("airport", QueryStringCollection.Get("airport"));
            routeValues.Add("Star1", QueryStringCollection.Get("Star1"));
            routeValues.Add("Star2", QueryStringCollection.Get("Star2"));
            routeValues.Add("Star3", QueryStringCollection.Get("Star3"));
            routeValues.Add("Star4", QueryStringCollection.Get("Star4"));
            routeValues.Add("Star5", QueryStringCollection.Get("Star5"));
            routeValues.Add("amenity", QueryStringCollection.Get("amenity"));

            AjaxOptions options = new AjaxOptions()
            {
                HttpMethod = "Get",
                OnSuccess = "Success_ajax_HotelSearch"
            };

            return AjaxExtensions.ActionLink(ajaxHelper, linkText, ActionName, ControllerName, routeValues, options, attr);
        }
    }
}