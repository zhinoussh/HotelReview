using HotelAdvice.Areas.Admin.Models;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.DataAccessLayer
{
    public class CacheRepository : DataRepository
    {
        private readonly string[] CityDependencyArray = { "CityCache" };
        private readonly string[] HotelDependencyArray = { "HotelCache" };
        private readonly string[] AmenityDependencyArray = { "AmenityCache" };

        private ICacheProvider _cache;

        public CacheRepository(ICacheProvider cache_provider, HotelAdviceDB dbContext)
           : base(dbContext)
        {
            _cache = cache_provider;
        }

         #region City
        
        public override void add_city(int id, string name, string attractions)
        {
            base.add_city(id, name, attractions);
           // _cache.InvalidateCache(CityDependencyArray);
        }

        public override List<CityViewModel> get_cities()
        {
            List<CityViewModel> result = _cache.GetOrSet<CityViewModel>("CityList", () => base.get_cities()
                                            , new TimeSpan(0, 30, 0), CityDependencyArray) as List<CityViewModel>;
          
            return result;
        }

        public override List<string> get_city_byId(int id)
        {
            return base.get_city_byId(id);
        }

        public override void delete_city(int id)
        {
            base.delete_city(id);
            _cache.InvalidateCache(CityDependencyArray);
        }

        #endregion City

        //#region Hotel

        //public override void add_hotel(HotelViewModel hotel)
        //{
            
        //}

        //public override void Save_Restaurants(string restaurants, int HotelId)
        //{
            
        //}

        //public override void Save_Rooms(string rooms, int HotelId)
        //{
            
        //}

        //public override void Save_Amenities(List<HotelAmenityViewModel> amenities, int HotelId)
        //{
            
        //}

        //public override void Save_Sighseeings(string sightseeing, int HotelId)
        //{
            
        //}

        //public override List<HotelViewModel> get_hotels()
        //{
            
        //}

        //public override HotelViewModel get_hotel_byId(int id)
        //{
            
        //}

        //public override HotelDetailViewModel get_hoteldetails(int id, string userId)
        //{
            
        //}

        //public override void delete_hotel(int id)
        //{
            
        //}

        //public override List<tbl_Restuarant> get_restaurants()
        //{
            
        //}

        //public override List<tbl_Restuarant> get_hotel_restaurants(int hotelID)
        //{
            
        //}

        //public override List<tbl_room_type> get_roomTypes()
        //{
            
        //}

        //public override List<tbl_room_type> get_hotel_rooms(int hotelID)
        //{
            
        //}

        //public override List<AmenityViewModel> get_Amenities()
        //{
            
        //}

        //public override List<HotelAmenityViewModel> get_Amenities_For_search(string selected_amenities)
        //{
            
        //}

        //public override void delete_Amenity(int id)
        //{
            
        //}

        //public override void add_amenity(int id, string amenity_name)
        //{
            
        //}

        //public override List<HotelAmenityViewModel> get_hotel_amenities(int hotelID)
        //{
            
        //}

        //public override List<tbl_sightseeing> get_Sightseeing()
        //{
            
        //}

        //public override List<tbl_sightseeing> get_hotel_sightseeings(int hotelID)
        //{
            
        //}

        //public override string get_hotel_name_by_photo(string photo_name)
        //{
            
        //}

        //public override string save_hotel_image(int hotel_id)
        //{
            
        //}

        //public override void delete_hotel_image(string photo_name)
        //{
            
        //}

        //#endregion Hotel

        //#region UserPage
        //public override int add_favorite_hotel(int hotel_id, string userId)
        //{
            
        //}

        //public override void remove_favorite_hotel(int hotel_id, string userId)
        //{
            
        //}

        //public override List<HotelSearchViewModel> get_wishList(string userId)
        //{
            
        //}

        //public override List<HotelSearchViewModel> get_reviewList(string userId)
        //{
            
        //}

        //public override void delete_review(int hotel_id, string userId)
        //{
            
        //}

        //public override void rate_hotel(int hotel_id, string userId, int rating)
        //{
            
        //}

        //public override List<HotelSearchViewModel> get_ratingList(string userId)
        //{
            
        //}

        //public override AddReviewViewModel get_previous_review(int hotelId, string userId)
        //{
            
        //}

        //public override void add_review(AddReviewViewModel review)
        //{
            
        //}

        //public override ReviewPageViewModel get_review_page(int hotelId)
        //{
            
        //}

        //public override List<ReviewListViewModel> get_reviews_for_hotel(int hotelId, ApplicationUserManager userMgr)
        //{
            
        //}

        //public override int get_rank_hotel(int hotelId)
        //{
            
        //}

        //public override List<CompareViewModel> get_compare_hotels_in_city(int cityID, int hotelId)
        //{
            
        //}

        //#endregion UserPage

        //#region HomePage
        //public override List<HotelSearchViewModel> Search_Hotels_in_city(int city_id, string userId)
        //{
            
        //}

        //public override List<HotelSearchViewModel> Search_Hotels_in_Detination(string destination_name, string userId)
        //{
            
        //}

        //public override List<HotelSearchViewModel> Advanced_Search(AdvancedSearchViewModel vm, string userId)
        //{
            
        //}

        //public override List<HotelSearchViewModel> Search_Popular_Hotels()
        //{
            
        //}

        //public override List<HotelSearchViewModel> Search_Top_Hotels()
        //{

        //}

        //#endregion HomePage

    }
}