using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;


namespace HotelAdvice
{
    public static class HtmlTextBox
    {

        /****************************************TextBox**************************************************/
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper,
        string name, string value, object htmlAttributes = null)
        {
            return BootstrapTextBox(htmlHelper, name, value, null, null, null, htmlAttributes);
        }

        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper,
        string name, string value, string cssClass, object htmlAttributes = null)
        {
            return BootstrapTextBox(htmlHelper, name, value, null, null, cssClass, htmlAttributes);
        }

        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper,
        string name, string value, string placeHolder, string title, object htmlAttributes = null)
        {
            return BootstrapTextBox(htmlHelper, name, value, placeHolder, title, null, htmlAttributes);

        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper,
         string name, string value, string placeHolder, string title, string cssClass, object htmlAttributes = null)
        {
            RouteValueDictionary rvd = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

             if (!string.IsNullOrEmpty(placeHolder))
                rvd.Add("placeHolder", placeHolder);

            if (!string.IsNullOrEmpty(title))
                rvd.Add("title", title);

            if (string.IsNullOrEmpty(cssClass))
                cssClass = "form-control";
            else
                cssClass = "fomr-control " + cssClass;

            rvd.Add("class", cssClass);

            return InputExtensions.TextBox(htmlHelper, name, value, rvd);

        }

       
        /********************************************TextBoxFor*******************************************/

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
                       Expression<Func<TModel, TValue>> bindingExpresion,
                       string name, object htmlAttributes = null)
        {
            return BootstrapTextBoxFor(htmlHelper, bindingExpresion, name, null, null, null, htmlAttributes);
        }


      
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
                          Expression<Func<TModel, TValue>> bindingExpresion,
                          string name, string placeHolder, string title, object htmlAttributes = null)
        {
            return BootstrapTextBoxFor(htmlHelper, bindingExpresion, name, placeHolder, title, null, htmlAttributes);
        }

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> bindingExpresion,
          string name, string cssClass, object htmlAttributes = null)
        {
            return BootstrapTextBoxFor(htmlHelper, bindingExpresion, name, null, null, cssClass, htmlAttributes);
        }

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> bindingExpresion,
            string name, string placeHolder,string title, string cssClass, object htmlAttributes = null)
        { 
            RouteValueDictionary rvd=new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            if(!string.IsNullOrEmpty(name))
                HtmlCommon.AddIdName(rvd, name, null);

            if (!string.IsNullOrEmpty(placeHolder))
                rvd.Add("placeHolder", placeHolder);

            if (!string.IsNullOrEmpty(title))
                rvd.Add("title", title);

            if (string.IsNullOrEmpty(cssClass))
                cssClass = "form-control";
            else
                cssClass = "fomr-control " + cssClass;

            rvd.Add("class", cssClass);


            return InputExtensions.TextBoxFor(htmlHelper, bindingExpresion, rvd);
        }
    }
}