using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.DataAccessLayer
{
    public interface IServiceLayer
    {

        IDataRepository DataLayer { get; set; }

        #region Hotel

        IPagedList<HotelViewModel> Get_HotelList(int? page, string filter);

        #endregion Hotel

    }
}