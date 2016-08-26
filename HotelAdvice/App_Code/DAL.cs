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

        #endregion Hotel

        #region Review

        #endregion Review

    }
}