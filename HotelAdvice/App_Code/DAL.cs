using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelAdvice.Models;
using HotelAdvice.ViewModels;

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
                    cityAttractions = c.CityAttractions
                }).OrderBy(c=>c.cityName).ToList();

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
            {
                new_obj = new tbl_Hotel();
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

                db.tbl_Hotel.Add(new_obj);
            }
            else
            {
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
                }
            }

            db.SaveChanges();

            //Save Restaurants
            if (hotel.HotelId != 0)
            {
                db.tbl_Hotel_Restaurants.RemoveRange(db.tbl_Hotel_Restaurants.Where(x => x.HotelID == hotel.HotelId));
                db.SaveChanges();
            }

            if (hotel.restaurants != null)
            {
                List<String> lst_restaurants = hotel.restaurants.Split(',').ToList<String>();
                foreach (string r in lst_restaurants)
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
                        Restaurant = rest,
                        HotelID = hotel.HotelId
                    });
                    db.SaveChanges();
                }

            }

        }

        public List<HotelViewModel> get_hotels()
        {
            List<HotelViewModel> lst_hotel = (from h in db.tbl_Hotel
                                             join c in db.tbl_city on h.CityId equals c.CityId
                                            select new HotelViewModel
                                            {
                                                HotelId  = h.HotelId,
                                                HotelName = h.HotelName,
                                                CityName=c.CityName,
                                                HotelStars = h.HotelStars
                                            }).OrderBy(x => x.HotelName).ToList();

            return lst_hotel;

        }
        
        public HotelViewModel get_hotel_byId(int id)
        {
           // List<string> hotel_prop = new List<string>();
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
                                              HotelAddress = h.HotelAddress
                                          }).FirstOrDefault();
                                     
           
            return hotel_prop;
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
            List<tbl_Restuarant> lst_restaurant = (from r in db.tbl_Hotel_Restaurants
                                                  join Rest in db.tbl_Restaurant on r.RestaurantID equals Rest.RestaurantID
                                                  where r.HotelID==hotelID
                                                  select Rest)
                                                   .OrderBy(r => r.RestaurantName).ToList();

            return lst_restaurant;

        }
       
        #endregion Hotel

        #region Review

        #endregion Review

    }
}