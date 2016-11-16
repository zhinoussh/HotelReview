using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace HotelAdvice.DataAccessLayer
{
    public class ServiceLayer: IServiceLayer 
    {
        private IDataRepository _dataLayer;
        const int _pageSize = 10;

         
        public IPagedList<HotelViewModel> Get_HotelList(int? page, string filter)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            List<HotelViewModel> lst_hotels = DataLayer.get_hotels();
            if (!String.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                lst_hotels = lst_hotels.Where(x =>
                                          (x.HotelName.ToLower().Contains(filter))
                                          ||
                                          (x.CityName.ToLower().Contains(filter))
                                          ||
                                          (!String.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(filter))).ToList();
            }

            //sort and set row number
            lst_hotels = lst_hotels.OrderBy(x => x.HotelName).Select((x, index) => new HotelViewModel
            {
                RowNum = index + 1,
                HotelId = x.HotelId,
                HotelName = x.HotelName,
                CityName = x.CityName,
                HotelStars = x.HotelStars
            }).ToList();

            IPagedList<HotelViewModel> paged_list = lst_hotels.ToPagedList(currentPageIndex, _pageSize);

            return paged_list;
        }

        IDataRepository DataLayer
        {
            get
            {
                if (_dataLayer == null)
                    _dataLayer = new DataRepository(new HotelAdviceDB());

                return _dataLayer;
            }
            set
            {
                _dataLayer = value;
            }
        }
    }
}