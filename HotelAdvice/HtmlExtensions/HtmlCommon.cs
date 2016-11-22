using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelAdvice
{
    public static class HtmlCommon
    {
        public enum ButtonType { 
            button,
            submit,
            reset
        }

        public static void AddIdName(TagBuilder tb, string name, string id)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = TagBuilder.CreateSanitizedId(name);

                if (string.IsNullOrEmpty(id))
                {
                    tb.MergeAttribute("id", name);
                }
                else
                    tb.MergeAttribute("id", TagBuilder.CreateSanitizedId(id));

                tb.MergeAttribute("name", name);
            }
        }
    }
}