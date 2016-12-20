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
        private readonly string[] CityDependency = { "CityList" };
        private readonly string[] HotelListDependency = { "HotelList" };        
        private readonly string[] RestaurantListDependency = { "RestaurantList" };
        private readonly string[] RoomListDependency = { "RoomList" };
        private readonly string[] SightSeeingListDependency = { "SightSeeingList" };
        private readonly string[] AmenityListDependency = { "AmenityList" };

        private readonly string HotelDetailKey = "HotelDetail";
        private readonly string HotelSightSeeingKey = "HotelSightSeeing";
        private readonly string HotelRoomKey = "HotelRoom";
        private readonly string HotelRestaurantKey = "HotelRestaurant";
        private readonly string HotelAmenityKey = "HotelAmenity";
        private readonly string HotelPhotoListKey = "HotelPhoto";



        private ICacheProvider cache_provider;

        public ICacheProvider _cache
        {
            get {
                if (cache_provider == null)
                    cache_provider = new CacheProvider();
                return cache_provider; 
            }
            set { cache_provider = value; }
        }
        

        public CacheRepository(HotelAdviceDB dbContext)
           : base(dbContext)
        {
        }


         #region City
        
        public override void add_city(int id, string name, string attractions)
        {
            base.add_city(id, name, attractions);
            _cache.InvalidateCache(CityDependency[0]);
        }

        public override List<CityViewModel> get_cities()
        {
            List<CityViewModel> result = _cache.GetOrSet<CityViewModel>("CityList", () => base.get_cities()
                                            , new TimeSpan(0, 30, 0), CityDependency) as List<CityViewModel>;
          
            return result;
        }

        public override List<string> get_city_byId(int id)
        {
            return base.get_city_byId(id);
        }

        public override void delete_city(int id)
        {
            base.delete_city(id);
            _cache.InvalidateCache(CityDependency[0]);
        }

        #endregion City

        #region Hotel

        public override void add_hotel(HotelViewModel hotel)
        {
            base.add_hotel(hotel);
            _cache.InvalidateCache(HotelListDependency[0]);
            _cache.InvalidateCache(HotelDetailKey + hotel.HotelId);
            _cache.InvalidateCache(HotelPhotoListKey + hotel.HotelId);
        }

        public override void Save_Restaurants(string restaurants, int HotelId)
        {
            base.Save_Restaurants(restaurants,HotelId);
            _cache.InvalidateCache(RestaurantListDependency[0]);
            _cache.InvalidateCache(HotelRestaurantKey + HotelId);
        }

        public override void Save_Rooms(string rooms, int HotelId)
        {
            base.Save_Rooms(rooms, HotelId);
            _cache.InvalidateCache(RoomListDependency[0]);
            _cache.InvalidateCache(HotelRoomKey + HotelId);
        }

        public override void Save_Amenities(List<HotelAmenityViewModel> amenities, int HotelId)
        {
            base.Save_Amenities(amenities, HotelId);
            _cache.InvalidateCache(AmenityListDependency[0]);
            _cache.InvalidateCache(HotelAmenityKey + HotelId);
        }

        public override void Save_Sighseeings(string sightseeing, int HotelId)
        {
            base.Save_Sighseeings(sightseeing, HotelId);
            _cache.InvalidateCache(SightSeeingListDependency[0]);
            _cache.InvalidateCache(HotelSightSeeingKey + HotelId);
        }

        public override List<HotelViewModel> get_hotels()
        {
            List<HotelViewModel> result = _cache.GetOrSet<HotelViewModel>("HotelList", () => base.get_hotels()
                                            , new TimeSpan(0, 30, 0), HotelListDependency) as List<HotelViewModel>;

            return result;
        }

        public override HotelViewModel get_hotel_byId(int id)
        {
            return base.get_hotel_byId(id);
        }

        public override HotelDetailViewModel get_hoteldetails(int id, string userId)
        {
            return base.get_hoteldetails(id, userId);
        }

        public override List<string> get_hotel_PhotoList(int hotel_id)
        {
            string cacheKye=HotelPhotoListKey + hotel_id;
            List<string> result = _cache.GetOrSet<string>(cacheKye, () => base.get_hotel_PhotoList(hotel_id), new TimeSpan(0, 30, 0)
                                            , null) as List<string>;

           return result;
        }

        public override HotelDetailAccordionViewModel get_hotelAccordionDetails(int id)
        {
            string cacheKey=HotelDetailKey+id;
            HotelDetailAccordionViewModel result = _cache.GetOrSet<HotelDetailAccordionViewModel>(cacheKey
                                                    , () => base.get_hotelAccordionDetails(id)
                                                    , new TimeSpan(0, 30, 0)) as HotelDetailAccordionViewModel;

            return result;
        }

        public override void delete_hotel(int id)
        {
            base.delete_hotel(id);

            _cache.InvalidateCache(HotelListDependency[0]);
            _cache.InvalidateCache(HotelDetailKey + id);
            _cache.InvalidateCache(HotelPhotoListKey + id);
        }

        public override List<string> get_restaurants()
        {
            List<string> result = _cache.GetOrSet<string>("RestaurantList", () => base.get_restaurants(), new TimeSpan(0, 30, 0)
                                            , RestaurantListDependency) as List<string>;

            return result;
        }

        public override List<string> get_hotel_restaurants(int hotelID)
        {
            string cacheKey = HotelRestaurantKey + hotelID;
            List<string> result = _cache.GetOrSet<string>(cacheKey, () => base.get_hotel_restaurants(hotelID), new TimeSpan(0, 30, 0)
                                           , null) as List<string>;

            return result;
        }

        public override List<string> get_roomTypes()
        {
            List<string> result = _cache.GetOrSet<string>("RoomList", () => base.get_roomTypes(), new TimeSpan(0, 30, 0)
                                            , RoomListDependency) as List<string>;

            return result;
        }

        public override List<string> get_hotel_rooms(int hotelID)
        {
            string cacheKey = HotelRoomKey + hotelID;
            List<string> result = _cache.GetOrSet<string>(cacheKey, () => base.get_hotel_rooms(hotelID), new TimeSpan(0, 30, 0)
                                           , null) as List<string>;

            return result;
        }

        public override List<AmenityViewModel> get_Amenities()
        {
            List<AmenityViewModel> result = _cache.GetOrSet<AmenityViewModel>("AmenityList", () => base.get_Amenities(), new TimeSpan(0, 30, 0)
                                           , AmenityListDependency) as List<AmenityViewModel>;

            return result;
        }

        public override List<HotelAmenityViewModel> get_Amenities_For_search(string selected_amenities)
        {
            return base.get_Amenities_For_search(selected_amenities);
        }

        public override void delete_Amenity(int id)
        {
            base.delete_Amenity(id);
            _cache.InvalidateCache(AmenityListDependency[0]);
        }

        public override void add_amenity(int id, string amenity_name)
        {
            base.add_amenity(id, amenity_name);
            _cache.InvalidateCache(AmenityListDependency[0]);
        }

        public override List<HotelAmenityViewModel> get_hotel_amenities(int hotelID)
        {
            string cacheKey = HotelAmenityKey + hotelID;
            List<HotelAmenityViewModel> result = _cache.GetOrSet<HotelAmenityViewModel>(cacheKey, () => base.get_hotel_amenities(hotelID)
                                                    , new TimeSpan(0, 30, 0), null) as List<HotelAmenityViewModel>;

            return result;
        }

        public override List<string> get_Sightseeing()
        {
            List<string> result = _cache.GetOrSet<string>("SightseeingList", () => base.get_Sightseeing(), new TimeSpan(0, 30, 0)
                                          , SightSeeingListDependency) as List<string>;

            return result;
        }

        public override List<string> get_hotel_sightseeings(int hotelID)
        {
            string cacheKey = HotelSightSeeingKey + hotelID;
            List<string> result = _cache.GetOrSet<string>(cacheKey, () => base.get_hotel_sightseeings(hotelID), new TimeSpan(0, 30, 0)
                                           , null) as List<string>;

            return result;
        }

        public override string[] get_hotel_prop_by_photo(string photo_name)
        {
            return base.get_hotel_prop_by_photo(photo_name);
        }

        public override string save_hotel_image(int hotel_id)
        {
            string file_name=base.save_hotel_image(hotel_id);
            _cache.InvalidateCache(HotelPhotoListKey + hotel_id);

            return file_name;
        }

        public override void delete_hotel_image(int hotel_id,string photo_name)
        {
            base.delete_hotel_image(hotel_id,photo_name);
            _cache.InvalidateCache(HotelPhotoListKey + hotel_id);
        }

        #endregion Hotel

        #region UserPage
        public override int add_favorite_hotel(int hotel_id, string userId)
        {
            return base.add_favorite_hotel(hotel_id, userId);
        }

        public override void remove_favorite_hotel(int hotel_id, string userId)
        {
            base.remove_favorite_hotel(hotel_id, userId);
        }

        public override List<HotelSearchViewModel> get_wishList(string userId)
        {
            return base.get_wishList(userId);
        }

        public override List<HotelSearchViewModel> get_reviewList(string userId)
        {
            return base.get_reviewList(userId);
        }

        public override void delete_review(int hotel_id, string userId)
        {
            base.delete_review(hotel_id, userId);
        }

        public override void rate_hotel(int hotel_id, string userId, int rating)
        {
            base.rate_hotel(hotel_id, userId, rating);
        }

        public override List<HotelSearchViewModel> get_ratingList(string userId)
        {
            return base.get_ratingList(userId);
        }

        public override AddReviewViewModel get_previous_review(int hotelId, string userId)
        {
            return base.get_previous_review(hotelId,userId);
        }

        public override void add_review(AddReviewViewModel review)
        {
            base.add_review(review);
        }

        public override ReviewPageViewModel get_review_page(int hotelId)
        {
            return base.get_review_page(hotelId);
        }

        public override List<ReviewListViewModel> get_reviews_for_hotel(int hotelId, ApplicationUserManager userMgr)
        {
            return base.get_reviews_for_hotel(hotelId,userMgr);
        }

        public override int get_rank_hotel(int hotelId)
        {
            return base.get_rank_hotel(hotelId);
        }

        public override List<CompareViewModel> get_compare_hotels_in_city(int cityID, int hotelId)
        {
            return base.get_compare_hotels_in_city(cityID,hotelId);
        }

        #endregion UserPage

        #region HomePage
        public override List<HotelSearchViewModel> Search_Hotels_in_city(int city_id, string userId)
        {
            return base.Search_Hotels_in_city(city_id, userId);
        }

        public override List<HotelSearchViewModel> Search_Hotels_in_Detination(string destination_name, string userId)
        {
            return base.Search_Hotels_in_Detination(destination_name, userId);
        }

        public override List<HotelSearchViewModel> Advanced_Search(AdvancedSearchViewModel vm, string userId)
        {
            return base.Advanced_Search(vm, userId);
        }

        public override List<HotelSearchViewModel> Search_Popular_Hotels()
        {
            return base.Search_Popular_Hotels() ;
        }

        public override List<HotelSearchViewModel> Search_Top_Hotels()
        {
            List<HotelSearchViewModel> result = _cache.GetOrSet<HotelSearchViewModel>("TopHotelList", () => base.Search_Top_Hotels(), new TimeSpan(0, 30, 0)
                  ,HotelListDependency) as List<HotelSearchViewModel>;

            return result;
        }

        #endregion HomePage

    }
}