using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.App_Code
{
    public class DateTimeClass
    {
        public static string getToday()
        {
            string month = TwoDigitDateFormat(DateTime.Now.Month + "");
            string day = TwoDigitDateFormat(DateTime.Now.Day + "");

            return DateTime.Now.Year + "/" + month + "/" + day;
        }

        private static string TwoDigitDateFormat(string d)
        {
            if (d.Length == 1)
                d = "0" + d;
            return d;
        }

    }
}