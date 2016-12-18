using HotelAdvice.Areas.Admin.ViewModels;
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
        
        public void add_city(int id, string name, string attractions)
        {
        
            base.add_city(id, name, attractions);
            _cache.InvalidateCache(CityDependencyArray[0]);
        }

        public List<CityViewModel> get_cities()
        {
            string cacheKey = "CityList";
            var result = _cache.GetCacheItem("CityList") as List<CityViewModel>;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = base.get_cities().ToList();
                    _cache.SetCacheItem(cacheKey, result, new TimeSpan(0, 30, 0), CityDependencyArray);
                }
            }
            return result;
        }

        public List<string> get_city_byId(int id)
        {
            
        }

        public void delete_city(int id)
        {
            
        }

        #endregion City
        public void add_hotel(Areas.Admin.ViewModels.HotelViewModel hotel)
        {
            
        }

        public void Save_Restaurants(string restaurants, int HotelId)
        {
            
        }

        public void Save_Rooms(string rooms, int HotelId)
        {
            
        }

        public void Save_Amenities(List<Areas.Admin.ViewModels.HotelAmenityViewModel> amenities, int HotelId)
        {
            
        }

        public void Save_Sighseeings(string sightseeing, int HotelId)
        {
            
        }

        public List<Areas.Admin.ViewModels.HotelViewModel> get_hotels()
        {
            
        }

        public Areas.Admin.ViewModels.HotelViewModel get_hotel_byId(int id)
        {
            
        }

        public Areas.WebSite.ViewModels.HotelDetailViewModel get_hoteldetails(int id, string userId)
        {
            
        }

        public void delete_hotel(int id)
        {
            
        }

        public List<Areas.Admin.Models.tbl_Restuarant> get_restaurants()
        {
            
        }

        public List<Areas.Admin.Models.tbl_Restuarant> get_hotel_restaurants(int hotelID)
        {
            
        }

        public List<Areas.Admin.Models.tbl_room_type> get_roomTypes()
        {
            
        }

        public List<Areas.Admin.Models.tbl_room_type> get_hotel_rooms(int hotelID)
        {
            
        }

        public List<Areas.Admin.ViewModels.AmenityViewModel> get_Amenities()
        {
            
        }

        public List<Areas.Admin.ViewModels.HotelAmenityViewModel> get_Amenities_For_search(string selected_amenities)
        {
            
        }

        public void delete_Amenity(int id)
        {
            
        }

        public void add_amenity(int id, string amenity_name)
        {
            
        }

        public List<Areas.Admin.ViewModels.HotelAmenityViewModel> get_hotel_amenities(int hotelID)
        {
            
        }

        public List<Areas.Admin.Models.tbl_sightseeing> get_Sightseeing()
        {
            
        }

        public List<Areas.Admin.Models.tbl_sightseeing> get_hotel_sightseeings(int hotelID)
        {
            
        }

        public string get_hotel_name_by_photo(string photo_name)
        {
            
        }

        public string save_hotel_image(int hotel_id)
        {
            
        }

        public void delete_hotel_image(string photo_name)
        {
            
        }

        public int add_favorite_hotel(int hotel_id, string userId)
        {
            
        }

        public void remove_favorite_hotel(int hotel_id, string userId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> get_wishList(string userId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> get_reviewList(string userId)
        {
            
        }

        public void delete_review(int hotel_id, string userId)
        {
            
        }

        public void rate_hotel(int hotel_id, string userId, int rating)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> get_ratingList(string userId)
        {
            
        }

        public Areas.WebSite.ViewModels.AddReviewViewModel get_previous_review(int hotelId, string userId)
        {
            
        }

        public void add_review(Areas.WebSite.ViewModels.AddReviewViewModel review)
        {
            
        }

        public Areas.WebSite.ViewModels.ReviewPageViewModel get_review_page(int hotelId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.ReviewListViewModel> get_reviews_for_hotel(int hotelId, ApplicationUserManager userMgr)
        {
            
        }

        public int get_rank_hotel(int hotelId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.CompareViewModel> get_compare_hotels_in_city(int cityID, int hotelId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> Search_Hotels_in_city(int city_id, string userId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> Search_Hotels_in_Detination(string destination_name, string userId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> Advanced_Search(Areas.WebSite.ViewModels.AdvancedSearchViewModel vm, string userId)
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> Search_Popular_Hotels()
        {
            
        }

        public List<Areas.WebSite.ViewModels.HotelSearchViewModel> Search_Top_Hotels()
        {
            
        }
    }
}