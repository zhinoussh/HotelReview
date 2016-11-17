using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;
using System.Web.Mvc;

namespace HotelAdvice.DataAccessLayer
{
    public interface IServiceLayer
    {

        IDataRepository DataLayer { get; set; }

        #region HomePage

        HomeViewModel Get_HomePage();
        AdvancedSearchViewModel Set_Advanced_Search();

        #endregion HomePage


        #region Hotel
        IPagedList<HotelViewModel> Get_HotelList(int? page, string filter);
        
        HotelViewModel Get_AddNewHotel(Controller ctrl, int? id, int? page, string filter);

        void Post_AddNewHotel(HotelViewModel Hotel, Controller ctrl);

        HotelViewModel GetHotelDescription(int id);

        HotelViewModel Get_DeleteHotel(int id, int? page, string filter);

        void Post_DeleteHotel(HotelViewModel Hotel, Controller ctrl);

        HotelImagesViewModel Get_HotelImagesView(int id, Controller ctrl);

        HotelImagesViewModel Get_HotelPhoto(string photoName);

        void Post_AddHotelPhoto(HotelImagesViewModel vm, int hotel_ID, Controller ctrl);
        
        void Post_DeleteHotelPhoto(HotelImagesViewModel photo, Controller ctrl);

        #endregion Hotel

        #region City
        IPagedList<CityViewModel> Get_CityList(int? page, string filter);

        CityViewModel Get_AddNewCity(int? city_id, int? page, string filter);

        void Post_AddNewCity(CityViewModel city);

        CityViewModel GetCityDescription(int city_id);

        CityViewModel Get_DeleteCity(int city_id, int? page, string filter);

        void Post_DeleteCity(CityViewModel city);

        #endregion City

    }
}