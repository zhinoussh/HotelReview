using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HotelAdvice.DataAccessLayer
{
    public interface IServiceLayer
    {

        IDataRepository DataLayer { get; set; }

        #region HomePage

        HomeViewModel Get_HomePage(int?page);
        AdvancedSearchViewModel Set_Advanced_Search_Fields(string HotelName, int? cityId
            , int? center, int? airport, string score, bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string selected_amenities);

        List<string> search_destinations_by_prefix(string Prefix);

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

        HotelImagesViewModel Get_DeleteHotelPhoto(string photo_name);

        void Post_DeleteHotelPhoto(HotelImagesViewModel photo, Controller ctrl);



        #endregion Hotel

        #region City
        IPagedList<CityViewModel> Get_CityList(int? page, string filter);

        CityViewModel Get_AddNewCity(Controller ctrl, int? city_id, int? page, string filter);

        void Post_AddNewCity(CityViewModel city, Controller ctrl);

        CityViewModel GetCityDescription(int city_id);

        CityViewModel Get_DeleteCity(int city_id, int? page, string filter);

        void Post_DeleteCity(Controller ctrl, CityViewModel city);

        #endregion City

        #region Amenity
        IPagedList<AmenityViewModel> Get_AmenityList(int? page, string filter);

        void Post_AddNewAmenity(string amenity_name, string amenity_id, int? page);

        AmenityViewModel Get_DeleteAmenity(int amenity_id, int? page, string filter);

        void Post_DeleteAmenity(AmenityViewModel amenity);

        #endregion Amenity

        #region UserPage


        /******************************REVIEW PART*******************************/

        UserPageViewModel Get_UserProfilePage(string user_id, int? page);
        ReviewPageViewModel Get_ReviewPage(string user_id, Controller ctrl, int hotel_id, int? page);
        void Post_AddNewReview(AddReviewViewModel review);
        string Get_PartialReviewList(AddReviewViewModel review,string user_id, Controller ctrl);
        AddReviewViewModel Get_EditReview(string user_id, int hotel_id, int? page);

        DeleteReviewViewModel Get_DeleteReview(string user_id, int hotel_id, int? page);
        IPagedList<HotelSearchViewModel> Post_DeleteReview(DeleteReviewViewModel review, string user_id);

        /******************************RATE PART*******************************/
        void Post_RateHotel(string user_id, int your_rating,int hotel_id);

        string Get_PartialRatingList(Controller ctrl, string user_id, int? page);

        /******************************Favorite Hotel PART*******************************/

        string[] Post_AddToFavorite(string user_id, Controller ctrl, string destination_name, bool? citySearch, int hotel_id, int? city_id, int? page, string sort, string HotelName, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5,string amenity);

        string Post_AddToFavorite_Detail(string user_id,int hotel_id);

        IPagedList<HotelSearchViewModel> Post_DeleteFavorite(string user_id,int hotel_id, int? page);

        
        #endregion UserPage

        #region SearchPage

       
        HotelDetailViewModel Get_HotelDetails(string user_id,int hotel_id);

        SearchPageViewModel Get_SearchResults(string user_id, string destination_name, bool? citySearch, string HotelName, int? cityId, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star,string amenity);

        SearchPageViewModel Get_Hotels_in_Detination(string destination, string userId);

        IPagedList<HotelSearchViewModel> Get_PartialHotelResults(string user_id, string destination_name, bool? citySearch, int? cityId
                                                            , int? page, string sort, string HotelName, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string amenity);
        
        
        #endregion SearchPage

    }
}