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
        public void add_hotel(int id, string name, string desc,int cityID,int stars,string checkin,string checkout
            ,string tel,string fax,string website,string email,string address)
        {
            tbl_Hotel new_obj;

            if (id == 0)
            {
                new_obj = new tbl_Hotel();
                new_obj.HotelName = name;
                new_obj.CityId = cityID;
                new_obj.Description = desc;
                new_obj.HotelStars = stars;
                new_obj.checkin = checkin;
                new_obj.checkout = checkout;
                new_obj.Tel = tel;
                new_obj.Fax = fax;
                new_obj.Website = website;
                new_obj.Email = email;
                new_obj.HotelAddress = address;

                db.tbl_Hotel.Add(new_obj);
            }
            else
            {
                new_obj = db.tbl_Hotel.Find(id);
                if (new_obj != null)
                {
                    new_obj.HotelName = name;
                    new_obj.CityId = cityID;
                    new_obj.Description = desc;
                    new_obj.HotelStars = stars;
                    new_obj.checkin = checkin;
                    new_obj.checkout = checkout;
                    new_obj.Tel = tel;
                    new_obj.Fax = fax;
                    new_obj.Website = website;
                    new_obj.Email = email;
                    new_obj.HotelAddress = address;
                }
            }

            db.SaveChanges();

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
        public List<String> get_hotel_byId(int id)
        {
            List<string> hotel_prop = new List<string>();
            tbl_Hotel h = db.tbl_Hotel.Find(id);
            hotel_prop.Add(h.HotelName);
            hotel_prop.Add(h.Description);
            hotel_prop.Add(h.CityId+"");

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
        #endregion Hotel

        #region Review

        #endregion Review

    }
}