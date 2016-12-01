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
            string chk_id, chk_status, hd_id_amenity, hd_selected;

            // Add checkboxes
            for (int i = 0; i < lst_checkbox.Count; i++)
            {
                chk_id = "chk_amenity" + lst_checkbox[i].AmenityID;
                chk_status = lst_checkbox[i].hotel_selected == true ? "true" : "false";
                hd_id_amenity = name + "[" + i + "].AmenityID";
                hd_selected = name + "[" + i + "].hotel_selected";

                sb.Append("<div class='"+containerCss+"'>");
                sb.Append("<div class='" + checkboxCss + "'>");
                sb.Append("<input type='hidden' name='" + hd_id_amenity + "' value='" + lst_checkbox[i].AmenityID + "' />");
                
                rvd = new RouteValueDictionary();
                HtmlCommon.AddIdName(rvd, hd_selected, chk_id);
                sb.Append(InputExtensions.CheckBox(htmlHelper, hd_selected,rvd));
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