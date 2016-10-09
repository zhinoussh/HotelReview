using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.App_Code
{
    public class SortingPagingInfo
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}