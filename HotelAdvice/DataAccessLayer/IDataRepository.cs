using System;
using System.Collections.Generic;
using System.Linq;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using HotelAdvice.Areas.Admin.Models;
using HotelAdvice.Areas.WebSite.Models;
using Microsoft.AspNet.Identity;

namespace HotelAdvice.DataAccessLayer
{
    public interface IDataRepository
    {

        #region City

        void add_city(int id, string name, string attractions);        

        List<CityViewModel> get_cities();

        List<String> get_city_byId(int id);

        void delete_city(int id);
       
        #endregion City

        #region Hotel
        int add_hotel(HotelViewModel hotel);

        void Save_Restaurants(string restaurants, int HotelId);

        void Save_Rooms(string rooms, int HotelId);

        void Save_Amenities(List<HotelAmenityViewModel> amenities, int HotelId);

        void Save_Sighseeings(string sightseeing, int HotelId);

        List<HotelViewModel> get_hotels();

        HotelViewModel get_hotel_byId(int id);

        HotelDetailViewModel get_hoteldetails(int id, string userId);

        HotelDetailAccordionViewModel get_hotelAccordionDetails(int id);
        
        List<string> get_hotel_PhotoList(int hotel_id);

        void delete_hotel(int id);

        List<string> get_restaurants();

        List<string> get_hotel_restaurants(int hotelID);

        List<string> get_roomTypes();

        List<string> get_hotel_rooms(int hotelID);

        List<AmenityViewModel> get_Amenities();
        List<HotelAmenityViewModel> get_Amenities_For_search(string selected_amenities);


        void delete_Amenity(int id);

        void add_amenity(int id, string amenity_name);

        List<HotelAmenityViewModel> get_hotel_amenities(int hotelID);

        List<string> get_Sightseeing();

        List<string> get_hotel_sightseeings(int hotelID);

        string[] get_hotel_prop_by_photo(string photo_name);

        string save_hotel_image(int hotel_id);

        void delete_hotel_image(int hotel_id,string photo_name);

        #endregion Hotel

        #region UserPage

        int add_favorite_hotel(int hotel_id, string userId);


        void remove_favorite_hotel(int hotel_id, string userId);

        List<HotelSearchViewModel> get_wishList(string userId);

        List<HotelSearchViewModel> get_reviewList(string userId);

        void delete_review(int hotel_id, string userId);
        void rate_hotel(int hotel_id, string userId, int rating);

        List<HotelSearchViewModel> get_ratingList(string userId);

        AddReviewViewModel get_previous_review(int hotelId, string userId);

        void add_review(AddReviewViewModel review);

        ReviewPageViewModel get_review_page(int hotelId);

        List<ReviewListViewModel> get_reviews_for_hotel(int hotelId, ApplicationUserManager userMgr);

        int get_rank_hotel(int hotelId);

        List<CompareViewModel> get_compare_hotels_in_city(int cityID, int hotelId);


        #endregion UserPage

        #region Home Page

        List<HotelSearchViewModel> Search_Hotels_in_city(int city_id, string userId);

        List<HotelSearchViewModel> Search_Hotels_in_Detination(string destination_name, string userId);
        
        List<HotelSearchViewModel> Advanced_Search(AdvancedSearchViewModel vm, string userId);

        List<HotelSearchViewModel> Search_Popular_Hotels();

        List<HotelSearchViewModel> Search_Top_Hotels();

        #endregion Home Page
    }
}