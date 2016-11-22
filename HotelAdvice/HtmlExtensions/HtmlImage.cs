using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace HotelAdvice
{
    public static class HtmlImage
    {
        /// <summary>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="imgSrc"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>

        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string actionName, string controllerName,
        string imgSrc, object routeValues = null)
        {

            return ImageLink(htmlHelper, actionName, controllerName, imgSrc, string.Empty, string.Empty, routeValues);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string actionName, string controllerName,
          string imgSrc, string cssClass, object routeValues = null)
        {

            return ImageLink(htmlHelper, actionName, controllerName, imgSrc, string.Empty, cssClass, routeValues);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string actionName, string controllerName,
            string imgSrc, string altText, string cssClass, object routeValues=null)
        {
            //set hyperlink to hotelDetail Page

            UrlHelper urlhelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            string url = urlhelper.Action(actionName, controllerName, routeValues);
            TagBuilder tb_link = new TagBuilder("a");
            tb_link.MergeAttribute("target", "_blank");
            tb_link.MergeAttribute("href", url);
            
            //set img
            TagBuilder tb_img = new TagBuilder("img");

            string imgPath = imgSrc;
            if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(@imgPath)))
                imgPath= "../../Images/empty.gif";
            
            tb_img.MergeAttribute("src", imgPath);
            if (!string.IsNullOrEmpty(altText))
                tb_img.MergeAttribute("alt", altText);
            if (!string.IsNullOrEmpty(cssClass))
                tb_img.AddCssClass(cssClass);
            
            tb_link.InnerHtml= tb_img.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(tb_link.ToString());
        }
    }
}