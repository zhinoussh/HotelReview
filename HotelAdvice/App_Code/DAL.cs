using System;
using System.Collections.Generic;
using System.Linq;
using HotelAdvice.Areas.Admin.ViewModels;
using HotelAdvice.Areas.WebSite.ViewModels;
using HotelAdvice.Areas.Admin.Models;
using HotelAdvice.Areas.WebSite.Models;

namespace HotelAdvice.App_Code
{
    public class DAL
    {

        HotelAdviceDB db=new HotelAdviceDB();

        #region City

        public void add_city(int id, string name,string attractions)
        {
            tbl_City new_obj;

            if (id == 0)
            {
                new_obj = new tbl_City();
                new_obj.CityName = name;
                new_obj.CityAttractions = attractions;
                db.tbl_city.Add(new_obj);
            }
            else
            {
               new_obj= db.tbl_city.Find(id);
               if (new_obj != null)
               {
                   new_obj.CityName = name;
                   new_obj.CityAttractions = attractions;
               }
            }

            db.SaveChanges();

        }

        public List<CityViewModel> get_cities()
        {
            List<CityViewModel> lst_city =db.tbl_city.Select(c =>
                new CityViewModel
                {
                    cityID = c.CityId,
                    cityName = c.CityName,
                    cityAttractions = c.CityAttractions,
                    hotel_count = db.tbl_Hotel.Count(x => x.CityId == c.CityId)
                }).ToList();

            return lst_city;
        }
        public List<String> get_city_byId(int id)
        {
            List<string> city_prop=new List<string>();
            tbl_City c = db.tbl_city.Find(id);
            city_prop.Add(c.CityName);
            city_prop.Add(c.CityAttractions);

            return city_prop;
        }

        public void delete_city(int id)
        {
            tbl_City c=db.tbl_city.Find(id);
            if(c!=null)
            {
                db.tbl_city.Remove(c);
                db.SaveChanges();
            }
        }
        #endregion City

        #region Hotel
        public void add_hotel(HotelViewModel hotel)
        {
            tbl_Hotel new_obj;

            if (hotel.HotelId == 0)
                new_obj = new tbl_Hotel();
            else
                new_obj = db.tbl_Hotel.Find(hotel.HotelId);

            if (new_obj != null)
            {
                new_obj.HotelName = hotel.HotelName;
                new_obj.CityId = hotel.CityId;
                new_obj.Description = hotel.Description;
                new_obj.HotelStars = hotel.HotelStars;
                new_obj.checkin = hotel.checkin;
                new_obj.checkout = hotel.checkout;
                new_obj.Tel = hotel.Tel;
                new_obj.Fax = hotel.Fax;
                new_obj.Website = hotel.Website;
                new_obj.Email = hotel.Email;
                new_obj.HotelAddress = hotel.HotelAddress;
                new_obj.distance_airport = (float)hotel.distance_airport;
                new_obj.distance_citycenter = (float)hotel.distance_citycenter;
            }

            //this is insert
            if (hotel.HotelId == 0)
                db.tbl_Hotel.Add(new_obj);
            
            db.SaveChanges();

            //Save Restaurants,Rooms           
            Save_Restaurants(hotel.restaurants, hotel.HotelId);
            Save_Rooms(hotel.rooms, hotel.HotelId);
            Save_Amenities(hotel.amenities, hotel.HotelId);
            Save_Sighseeings(hotel.sightseeing, hotel.HotelId);
            

        }

        private void Save_Restaurants(string restaurants, int HotelId)
        {
            //Edit
            if (HotelId != 0)
            {
                db.tbl_Hotel_Restaurants.RemoveRange(db.tbl_Hotel_Restaurants.Where(x => x.HotelID == HotelId));
                db.SaveChanges();
            }

            List<String> temp_list = new List<string>();
            if (restaurants != null)
            {
                temp_list = restaurants.Split(',').ToList<String>();
                foreach (string r in temp_list)
                {
                    tbl_Restuarant rest = db.tbl_Restaurant.Where(x => x.RestaurantName == r).FirstOrDefault();
                    if (rest == null)
                    {
                        rest = new tbl_Restuarant() { RestaurantName = r };
                        db.tbl_Restaurant.Add(rest);
                        db.SaveChanges();
                    }

                    db.tbl_Hotel_Restaurants.Add(new tbl_Hotel_Restaurants()
                    {
                        RestaurantID = rest.RestaurantID,
                        HotelID = HotelId
                    });
                    db.SaveChanges();
                }

            }
        }

        private void Save_Rooms(string rooms, int HotelId)
        {
            //Edit
            if (HotelId != 0)
            {
                db.tbl_Hotel_Rooms.RemoveRange(db.tbl_Hotel_Rooms.Where(x => x.HotelID ==HotelId));
                db.SaveChanges();
            }

            List<String> temp_list = new List<string>();
            if (rooms != null)
            {
                temp_list = rooms.Split(',').ToList<String>();
                foreach (string r in temp_list)
                {
                    tbl_room_type room = db.tbl_Room_Type.Where(x => x.Room_Type == r).FirstOrDefault();
                    if (room == null)
                    {
                        room = new tbl_room_type() { Room_Type = r };
                        db.tbl_Room_Type.Add(room);
                        db.SaveChanges();
                    }

                    db.tbl_Hotel_Rooms.Add(new tbl_Hotel_Rooms()
                    {
                        RoomTypeID = room.RoomTypeID,
                        HotelID = HotelId
                    });
                    db.SaveChanges();
                }

            }
        }

        private void Save_Amenities(List<AmenityViewModel> amenities, int HotelId)
        {
            //Edit
            if (HotelId != 0)
            {
                db.tbl_Hotel_Amenities.RemoveRange(db.tbl_Hotel_Amenities.Where(x => x.HotelID == HotelId));
                db.SaveChanges();
            }

            foreach (AmenityViewModel item in amenities)
            {
                if (item.hotel_selected == true)
                {
                    db.tbl_Hotel_Amenities.Add(new tbl_hotel_amenity()
                    {
                        AmenityID = item.AmenityID,
                        HotelID = HotelId
                    });
                }
                db.SaveChanges();
            }


        }

        private void Save_Sighseeings(string sightseeing, int HotelId)
        {
            //Edit
            if (HotelId != 0)
            {
                db.tbl_Hotel_Sightseeings.RemoveRange(db.tbl_Hotel_Sightseeings.Where(x => x.HotelID == HotelId));
                db.SaveChanges();
            }

            List<String> temp_list = new List<string>();
            if (sightseeing != null)
            {
                temp_list = sightseeing.Split(',').ToList<String>();
                foreach (string r in temp_list)
                {
                    tbl_sightseeing sight = db.tbl_Sightseeing.Where(x => x.Sightseeing_Type == r).FirstOrDefault();
                    if (sight == null)
                    {
                        sight = new tbl_sightseeing() { Sightseeing_Type = r };
                        db.tbl_Sightseeing.Add(sight);
                        db.SaveChanges();
                    }

                    db.tbl_Hotel_Sightseeings.Add(new tbl_hotel_sightseeing()
                    {
                        SightseeingID = sight.SightseeingID,
                        HotelID = HotelId
                    });
                    db.SaveChanges();
                }

            }
        }

        public List<HotelViewModel> get_hotels()
        {
            List<HotelViewModel> lst_hotel = db.tbl_Hotel.Join(db.tbl_city, h => h.CityId, c => c.CityId, (hotel, city) =>
                new HotelViewModel
                {
                    HotelId = hotel.HotelId,
                    HotelName = hotel.HotelName,
                    CityName = city.CityName,
                    HotelStars = hotel.HotelStars
                }).ToList();

            return lst_hotel;

        }
        
        public HotelViewModel get_hotel_byId(int id)
        {
           HotelViewModel hotel_prop = (from h in db.tbl_Hotel
                                         where h.HotelId==id
                                          select new HotelViewModel
                                          {
                                              HotelId=h.HotelId,
                                              HotelName = h.HotelName,
                                              CityId = h.CityId,
                                              Description = h.Description,
                                              HotelStars = h.HotelStars,
                                              checkin = h.checkin,
                                              checkout = h.checkout,
                                              Tel = h.Tel,
                                              Fax = h.Fax,
                                              Website = h.Website,
                                              Email = h.Email,
                                              HotelAddress = h.HotelAddress,
                                              distance_airport=h.distance_airport,
                                              distance_citycenter = h.distance_citycenter 
                                          }).FirstOrDefault();
                                     
           
            return hotel_prop;
        }

        public HotelDetailViewModel get_hoteldetails(int id,string userId)
        {
            HotelDetailViewModel detail = (from h in db.tbl_Hotel.Where(x => x.HotelId == id)
                                           join c in db.tbl_city on h.CityId.Value equals (int?)c.CityId
                                           join w in db.tbl_Wish_List.Where(x=>x.UserId==userId) 
                                           on h.HotelId equals w.HotelId into Wishist
                                           join r in db.tbl_Rating.Where(x=>x.UserId==userId) 
                                           on h.HotelId equals r.HotelId into YourRating
                                           from ww in Wishist.DefaultIfEmpty()
                                           from rr in YourRating.DefaultIfEmpty()
                                           select new HotelDetailViewModel
                                           {
                                               HotelId=h.HotelId,
                                               HotelName = h.HotelName,
                                               HotelStars = h.HotelStars,
                                               CityName = c.CityName,
                                               YourRating=(rr==null?0:rr.rating),
                                               review_num=200,
                                               is_favorite=(ww==null?false:true),
                                               Description = h.Description,
                                               checkin = h.checkin,
                                               checkout = h.checkout,
                                               Tel = h.Tel,
                                               Fax = h.Fax,
                                               Website = h.Website,
                                               Email = h.Email,
                                               HotelAddress = h.HotelAddress,
                                               distance_airport = h.distance_airport,
                                               distance_citycenter = h.distance_citycenter
                                           }).FirstOrDefault();

            string rating_avg="0";
            var query_rating=db.tbl_Rating.Where(x => x.HotelId == id);
            if(query_rating.Any())
                 rating_avg= query_rating.Average(x => x.rating).ToString();

            detail.GuestRating = float.Parse(rating_avg);

            detail.photos = db.tbl_Hotel_Photo.Where(x => x.HotelID == id).Select(x => x.photo_name).ToList<String>();

            detail.rooms  = (from r in db.tbl_Hotel_Rooms.Where(x => x.HotelID == id)
                                     join t in db.tbl_Room_Type on r.RoomTypeID equals t.RoomTypeID
                                     select t.Room_Type).ToList();

            detail.restaurants = (from r in db.tbl_Hotel_Restaurants.Where(x => x.HotelID == id)
                            join t in db.tbl_Restaurant on r.RestaurantID equals t.RestaurantID
                            select t.RestaurantName).ToList();

            detail.sightseeing = (from r in db.tbl_Hotel_Sightseeings.Where(x => x.HotelID == id)
                                  join t in db.tbl_Sightseeing on r.SightseeingID equals t.SightseeingID
                                  select t.Sightseeing_Type).ToList();

            detail.amenities = get_hotel_amenities(id);

            return detail;
            
        }
        public void delete_hotel(int id)
        {
            tbl_Hotel c = db.tbl_Hotel.Find(id);
            if (c != null)
            {
                db.tbl_Hotel.Remove(c);
                db.SaveChanges();
            }
        }

        public List<tbl_Restuarant> get_restaurants()
        {
            List<tbl_Restuarant> lst_restaurant = db.tbl_Restaurant.Select(r =>r)
                .OrderBy(r => r.RestaurantName).ToList();

            return lst_restaurant;
            
        }

        public List<tbl_Restuarant> get_hotel_restaurants(int hotelID)
        {
            List<tbl_Restuarant> lst_restaurant = (from r in db.tbl_Hotel_Restaurants.Where(x=>x.HotelID==hotelID)
                                                  join Rest in db.tbl_Restaurant on r.RestaurantID equals Rest.RestaurantID
                                                   select Rest)
                                                   .OrderBy(r => r.RestaurantName).ToList();

            return lst_restaurant;

        }

        public List<tbl_room_type> get_roomTypes()
        {
            List<tbl_room_type> lst_rooms= db.tbl_Room_Type.Select(r => r)
                .OrderBy(r => r.Room_Type).ToList();

            return lst_rooms;
            
        }

        public List<tbl_room_type> get_hotel_rooms(int hotelID)
        {
            List<tbl_room_type> lst_rooms = (from t in db.tbl_Hotel_Rooms.Where(r => r.HotelID == hotelID)
                                             join r in db.tbl_Room_Type on t.RoomTypeID equals r.RoomTypeID
                                             select r)
                                          .OrderBy(x => x.Room_Type).ToList();
            return lst_rooms;
        }

        public List<AmenityViewModel> get_Amenities()
        {
            List<AmenityViewModel> lst_amenity = db.tbl_Amenity.Select(r=>new AmenityViewModel { 
                AmenityID=r.AmenityID,
                AmenityName=r.AmenityName
            }).ToList();

            return lst_amenity;

        }


        public void delete_Amenity(int id)
        {
           tbl_amenity a= db.tbl_Amenity.Find(id);
           if (a != null)
           {
               db.tbl_Amenity.Remove(a);
               db.SaveChanges();
           }
        }

        public void add_amenity(int id, string amenity_name)
        {
            tbl_amenity new_obj;

            if (id == 0)
            {
                new_obj = new tbl_amenity();
                new_obj.AmenityName = amenity_name;
                db.tbl_Amenity.Add(new_obj);
            }
            else
            {
                new_obj = db.tbl_Amenity.Find(id);
                if (new_obj != null)
                {
                    new_obj.AmenityName = amenity_name;
                }
            }

            db.SaveChanges();
        }
       
        public List<AmenityViewModel> get_hotel_amenities(int hotelID)
        {
            List<AmenityViewModel> lst_amenities = (from a in db.tbl_Amenity
                                                    join h in db.tbl_Hotel_Amenities.Where(x => x.HotelID == hotelID) 
                                                    on a.AmenityID equals h.AmenityID
                                                    into Hotel_Amenity
                                                    from x in Hotel_Amenity.DefaultIfEmpty()
                                                    select new AmenityViewModel { 
                                                        AmenityID=a.AmenityID,
                                                        AmenityName=a.AmenityName,
                                                        hotel_selected=(x==null? false :true)
                                                    }).OrderBy(x => x.AmenityName).ToList();
            return lst_amenities;
        }

        public List<tbl_sightseeing> get_Sightseeing()
        {
            List<tbl_sightseeing> lst_sightseeing = db.tbl_Sightseeing.Select(r => r)
                .OrderBy(r => r.Sightseeing_Type).ToList();

            return lst_sightseeing;

        }

        public List<tbl_sightseeing> get_hotel_sightseeings(int hotelID)
        {
            List<tbl_sightseeing> lst_sightseeing = (from h in db.tbl_Hotel_Sightseeings.Where(r => r.HotelID == hotelID)
                                             join s in db.tbl_Sightseeing on h.SightseeingID equals s.SightseeingID
                                             select s)
                                          .OrderBy(x => x.Sightseeing_Type).ToList();
            return lst_sightseeing;
        }
        
        public string save_hotel_image(int hotel_id)
        {
            string file_name="";
            string hotel_name=db.tbl_Hotel.Where(x=>x.HotelId==hotel_id).Select(x=>x.HotelName).FirstOrDefault();
            if(hotel_name!=null)
            {
               tbl_Hotel_Photo last_photo= db.tbl_Hotel_Photo.Where(x => x.HotelID == hotel_id).OrderByDescending(x => x.PhotoID).FirstOrDefault();
               if (last_photo!=null)
               {
                   int index_from = last_photo.photo_name.LastIndexOf('_') + 1;
                   int index_to =  last_photo.photo_name.LastIndexOf('.');
                   int photo_num = Int32.Parse(last_photo.photo_name.Substring(index_from, index_to - index_from)) + 1;
                   file_name = hotel_name + "_" + photo_num + ".jpg";
                  
               }
               else
                   file_name = hotel_name + "_1.jpg";

               db.tbl_Hotel_Photo.Add(new tbl_Hotel_Photo
               {
                   photo_name = file_name,
                   HotelID = hotel_id
               });
               db.SaveChanges();
            }

            return file_name;
        }



        public void delete_hotel_image(string photo_name)
        {
            tbl_Hotel_Photo c = db.tbl_Hotel_Photo.Where(x=>x.photo_name==photo_name).FirstOrDefault();
            if (c != null)
            {
                db.tbl_Hotel_Photo.Remove(c);
                db.SaveChanges();
            }
        }

        #endregion Hotel

        #region UserPage
        
        public int add_favorite_hotel(int hotel_id,string userId)
        {
           tbl_WishList w= db.tbl_Wish_List.Where(x => x.HotelId == hotel_id && x.UserId == userId).FirstOrDefault();

           if (w == null)
           {
               w = new tbl_WishList() { UserId = userId, HotelId = hotel_id };
               db.tbl_Wish_List.Add(w);
               db.SaveChanges();
               return 1;
           }
           else
           {
               db.tbl_Wish_List.Remove(w);
               db.SaveChanges();
               return 0;
           }
            
        }

        public void remove_favorite_hotel(int hotel_id, string userId)
        {
            tbl_WishList w = db.tbl_Wish_List.Where(x => x.HotelId == hotel_id && x.UserId == userId).FirstOrDefault();

            db.tbl_Wish_List.Remove(w);
            db.SaveChanges();
        }

        public List<HotelSearchViewModel> get_wishList(string userId)
        {
            List<HotelSearchViewModel> lst_result = (from w in db.tbl_Wish_List.Where(x => x.UserId == userId)
                                                     join h in db.tbl_Hotel
                                                     on w.HotelId equals h.HotelId 
                                                     select new HotelSearchViewModel
                                                     {
                                                         HotelId = h.HotelId,
                                                         HotelName = h.HotelName,
                                                         Website = h.Website,
                                                         HotelStars = h.HotelStars,
                                                         num_reviews = 999
                                                     }).ToList();

            lst_result = Set_Guest_Rating(lst_result);

            return lst_result;
        }


        public void rate_hotel(int hotel_id, string userId,int rating)
        {
            tbl_rating r = db.tbl_Rating.Where(x => x.HotelId == hotel_id && x.UserId == userId).FirstOrDefault();

           if (r == null)
           {
               r = new tbl_rating() { UserId = userId, HotelId = hotel_id,rating=rating };
               db.tbl_Rating.Add(r);
               db.SaveChanges();
           }
           else
           {
               r.rating = rating;
               db.SaveChanges();
           }
            
        }

        public List<HotelSearchViewModel> get_ratingList(string userId)
        {
            List<HotelSearchViewModel> lst_result = (from r in db.tbl_Rating.Where(x => x.UserId == userId)
                                                     join h in db.tbl_Hotel
                                                     on r.HotelId equals h.HotelId
                                                     select new HotelSearchViewModel
                                                     {
                                                         HotelId = h.HotelId,
                                                         HotelName = h.HotelName,
                                                         Website = h.Website,
                                                         HotelStars = h.HotelStars,
                                                         num_reviews = 999,
                                                         GuestRating = 3,
                                                         YourRating=r.rating
                                                     }).ToList();

            lst_result = Set_Guest_Rating(lst_result);


            return lst_result;
        }


        public ReviewPageViewModel get_reviews(int hotelId)
        {
            ReviewPageViewModel result = new ReviewPageViewModel();

            HotelReviewViewModel detail = (from h in db.tbl_Hotel.Where(x => x.HotelId == hotelId)
                                           join c in db.tbl_city on h.CityId.Value equals (int?)c.CityId
                                           select new HotelReviewViewModel
                                           {
                                               HotelId=h.HotelId,
                                               HotelName = h.HotelName,
                                               HotelStars = h.HotelStars,
                                               Address = h.HotelAddress,
                                               CityId=c.CityId,
                                               CityName=c.CityName,
                                               num_reviews=200,
                                               Website = h.Website,
                                               Email = h.Email,
                                               Tel=h.Tel,
                                               hotel_count_city = db.tbl_Hotel.Count(m => m.CityId == c.CityId)
                                           }).FirstOrDefault();



            string rating_avg="0";
            var query_rating = db.tbl_Rating.Where(x => x.HotelId == hotelId);
            if(query_rating.Any())
                 rating_avg= query_rating.Average(x => x.rating).ToString();

            detail.GuestRating = float.Parse(rating_avg);

            detail.rank_hotel= get_rank_hotel(hotelId);
         
            result.hotel_properties = detail;

            //**********set scoreviewModel*************/
            ScoreViewModel scores = new ScoreViewModel();
            scores.avg_total_rating = float.Parse(rating_avg);
            scores.avg_Cleanliness_rating = float.Parse("1.5");
            scores.avg_Comfort_rating = float.Parse("4.5");
            scores.avg_Facilities_rating = float.Parse("5");
            scores.avg_Location_rating = float.Parse("5");
            scores.avg_Staff_rating = float.Parse("3.5");
            scores.avg_Value_for_money_rating= float.Parse("2.5");

            result.hotel_scores = scores;

            //***************Compare Hotel list***************/
            result.lst_compare_hotels = get_compare_hotels_in_city(detail.CityId,hotelId);

            return result;
        }

        private int get_rank_hotel(int hotelId)
        {

            var query = (from r in db.tbl_Rating
                         group r by r.HotelId into rate_g
                         select new
                         {
                             avg_rate = rate_g.Average(x => x.rating),
                             hotelId = rate_g.Key
                         }).AsEnumerable();

            int rank = query.OrderByDescending(x => x.avg_rate)
                   .Select((x, index) => new { rank = index + 1, hotelid = x.hotelId })
                   .Where(x => x.hotelid == hotelId)
                   .Select(x => x.rank).FirstOrDefault();

            if (rank == 0)
                rank = 1;

            return rank;
        }

        private List<CompareViewModel> get_compare_hotels_in_city(int cityID, int hotelId)
        {
            List<CompareViewModel> lst_result = (from h in db.tbl_Hotel.Where(x => x.CityId == cityID && x.HotelId != hotelId)
                                                     join r in db.tbl_Rating on h.HotelId equals r.HotelId into Rating
                                                     from rr in Rating.DefaultIfEmpty()
                                                     group rr by new { rr.HotelId, h.HotelName,h.HotelStars } into g
                                                     select new CompareViewModel
                                                     {
                                                         HotelId = g.Key.HotelId.Value,
                                                         HotelName = g.Key.HotelName,
                                                         avg_total_rating = (float)g.Average(x => x.rating),
                                                         avg_Location_rating = (float)g.Average(x => x.Location_rating),
                                                         avg_Cleanliness_rating = (float)g.Average(x => x.Cleanliness_rating),
                                                         avg_Value_for_money_rating = (float)g.Average(x => x.Value_for_money_rating),
                                                     }).ToList();


            return lst_result;
        }
        

        #endregion UserPage

        #region Home Page

        public List<HotelSearchViewModel> Search_Hotels_in_city(int city_id,string userId)
        {
            List<HotelSearchViewModel> lst_result = (from h in db.tbl_Hotel.Where(x => x.CityId == city_id)
                                                     join w in db.tbl_Wish_List.Where(x => x.UserId == userId)
                                                     on h.HotelId equals w.HotelId into WishList
                                                     from ww in WishList.DefaultIfEmpty()
                                                    select new HotelSearchViewModel
                                                        {
                                                            HotelId = h.HotelId,
                                                            HotelName = h.HotelName,
                                                            Website = h.Website,
                                                            HotelStars = h.HotelStars,
                                                            Description = h.Description,
                                                            distance_citycenter=h.distance_citycenter,
                                                            num_reviews=999,
                                                            is_favorite=(ww==null?false:true)
                                                        }).ToList();

            lst_result=Set_Guest_Rating(lst_result);

            return lst_result;
        }

        private List<HotelSearchViewModel> Set_Guest_Rating(List<HotelSearchViewModel> lst_hotels)
        {
            string rating_avg;
            foreach (var item in lst_hotels)
            {
                rating_avg = "0";
                var query_rating = db.tbl_Rating.Where(x => x.HotelId == item.HotelId);
                if (query_rating.Any())
                    rating_avg = query_rating.Average(x => x.rating).ToString();

                item.GuestRating = float.Parse(rating_avg);
            }

            return lst_hotels;
        }

        #endregion Home Page


    }
}