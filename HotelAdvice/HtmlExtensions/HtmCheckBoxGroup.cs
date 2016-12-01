using HotelAdvice.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HotelAdvice
{
    public static class HtmCheckBoxGroup
    {
        public static MvcHtmlString AwesomeCheckboxGroupFor(this HtmlHelper htmlHelper,
            string name,List<HotelAmenityViewModel> lst_checkbox, string checkboxCss,string containerCss, Object htmlAttributes = null)
        {

            StringBuilder sb = new StringBuilder();
            RouteValueDictionary rvd;

            if (!string.IsNullOrEmpty(checkboxCss))
                checkboxCss = "checkbox " + checkboxCss;
            else
                checkboxCss = "checkbox";
            string chk_id, name_amenity_id, name_checkbox;

            // Add checkboxes
            for (int i = 0; i < lst_checkbox.Count; i++)
            {
                chk_id = "chk_amenity" + lst_checkbox[i].AmenityID;
                name_amenity_id = name + "[" + i + "].AmenityID";
                name_checkbox = name + "[" + i + "].hotel_selected";

                sb.Append("<div class='"+containerCss+"'>");
                sb.Append("<div class='" + checkboxCss + "'>");
                sb.Append("<input type='hidden' name='" + name_amenity_id + "' value='" + lst_checkbox[i].AmenityID + "' />");
                
                rvd = new RouteValueDictionary();
                HtmlCommon.AddIdName(rvd, name_checkbox, chk_id);
                sb.Append(InputExtensions.CheckBox(htmlHelper, name_checkbox, lst_checkbox[i].hotel_selected, rvd));
                sb.Append("<label for='" + chk_id + "' class='text-small'>");
                sb.Append(lst_checkbox[i].AmenityName);
                sb.Append("</label>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            return MvcHtmlString.Create(sb.ToString());


        }
    }
}