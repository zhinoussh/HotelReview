using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using PagedList.Mvc;
using HotelAdvice.Areas.Admin.Models;
using System.Web.Mvc;
using System.IO;
using HotelAdvice.Areas.WebSite.ViewModels;


namespace HotelAdvice.DataAccessLayer
{
    public class ServiceLayer: IServiceLayer 
    {
        private IDataRepository _dataLayer;
        const int pageSize = 10;

        public IDataRepository DataLayer
        {
            get
            {
                if (_dataLayer == null)
                    _dataLayer = new DataRepository(new HotelAdviceDB());

                return _dataLayer;
            }
            set
            {
                _dataLayer = value;
            }
        }


        #region HomePage

        public HomeViewModel Get_HomePage() {
            HomeViewModel vm = new HomeViewModel();
            vm.lst_city = DataLayer.get_cities().OrderBy(x => x.cityName).ToList();

            // List<CityViewModel> search_city=vm.lst_city;
            // search_city.Add(new CityViewModel{cityID=0,cityName="Select City"});

            //set advanced search view model
            vm.Advanced_Search = Set_Advanced_Search();

            return vm;
        }

        public AdvancedSearchViewModel Set_Advanced_Search()
        {
            AdvancedSearchViewModel vm_search = new AdvancedSearchViewModel();
            List<CityViewModel> lst_city = DataLayer.get_cities().OrderBy(x => x.cityName).ToList();
            List<CityViewModel> search_city = lst_city;
            search_city.Add(new CityViewModel { cityID = 0, cityName = "Select City" });
            vm_search.City_List = new SelectList(search_city, "cityID", "cityName");
            vm_search.selected_city = 0;


            List<KeyValuePair<int, string>> lst_locations = new List<KeyValuePair<int, string>>();
            lst_locations.Add(new KeyValuePair<int, string>(0, "Any Kilometer"));
            lst_locations.Add(new KeyValuePair<int, string>(1, "less than 1 km"));
            lst_locations.Add(new KeyValuePair<int, string>(2, "less than 2 km"));
            lst_locations.Add(new KeyValuePair<int, string>(3, "less than 3 km"));
            lst_locations.Add(new KeyValuePair<int, string>(4, "less than 4 km"));
            lst_locations.Add(new KeyValuePair<int, string>(5, "less than 5 km"));
            lst_locations.Add(new KeyValuePair<int, string>(10, "less than 10 km"));
            lst_locations.Add(new KeyValuePair<int, string>(20, "less than 20 km"));
            vm_search.Location = new SelectList(lst_locations, "Key", "Value");
            vm_search.distance_city_center = 0;
            vm_search.distance_airport = 0;

            vm_search.lst_amenity = DataLayer.get_Amenities();

            return vm_search;
        }

        #endregion HomePage

        #region Hotel

        public IPagedList<HotelViewModel> Get_HotelList(int? page, string filter)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            List<HotelViewModel> lst_hotels = DataLayer.get_hotels();
            if (!String.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                lst_hotels = lst_hotels.Where(x =>
                                          (x.HotelName.ToLower().Contains(filter))
                                          ||
                                          (x.CityName.ToLower().Contains(filter))
                                          ||
                                          (!String.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(filter))).ToList();
            }

            //sort and set row number
            lst_hotels = lst_hotels.OrderBy(x => x.HotelName).Select((x, index) => new HotelViewModel
            {
                RowNum = index + 1,
                HotelId = x.HotelId,
                HotelName = x.HotelName,
                CityName = x.CityName,
                HotelStars = x.HotelStars
            }).ToList();

            IPagedList<HotelViewModel> paged_list = lst_hotels.ToPagedList(currentPageIndex, pageSize);

            return paged_list;
        }

        public HotelViewModel Get_AddNewHotel(Controller ctrl, int? id, int? page, string filter)
        {
            HotelViewModel vm = new HotelViewModel();
            vm.HotelId = id.HasValue ? id.Value : 0;

            List<CityViewModel> cities = DataLayer.get_cities();
            vm.CityId = cities.First().cityID;
            vm.imgPath = "/images/empty.gif?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
            //this is edit
            if (vm.HotelId != 0)
            {
                vm = DataLayer.get_hotel_byId(vm.HotelId);

                string path = "/Upload/" + vm.HotelName + "/main.jpg";
                if (System.IO.File.Exists(ctrl.Server.MapPath(@path)))
                    vm.imgPath = path + "?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                else
                    vm.imgPath = "/images/empty.gif?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");

                //add restaurants
                List<tbl_Restuarant> lst_rest = DataLayer.get_hotel_restaurants(vm.HotelId);
                if (lst_rest.Count > 0)
                {
                    vm.restaurants = string.Join(",", lst_rest.Select(x => x.RestaurantName));
                }

                //add rooms
                List<tbl_room_type> lst_room = DataLayer.get_hotel_rooms(vm.HotelId);
                if (lst_room.Count > 0)
                {
                    vm.rooms = string.Join(",", lst_room.Select(x => x.Room_Type));
                }


                //add sightseeings
                List<tbl_sightseeing> lst_sightseeing = DataLayer.get_hotel_sightseeings(vm.HotelId);
                if (lst_sightseeing.Count > 0)
                {
                    vm.sightseeing = string.Join(",", lst_sightseeing.Select(x => x.Sightseeing_Type));
                }
            }

            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";
            vm.lst_city = new SelectList(cities, "cityID", "cityName");
            vm.amenities = DataLayer.get_hotel_amenities(vm.HotelId);

            return vm;
        }

        public void Post_AddNewHotel(HotelViewModel Hotel, Controller ctrl)
        {
            Hotel.distance_citycenter = Hotel.distance_citycenter != null ? Hotel.distance_citycenter : 0;
            Hotel.distance_airport = Hotel.distance_airport != null ? Hotel.distance_airport : 0;

            string original_hotel_name = "";
            if (Hotel.HotelId != 0)
                original_hotel_name = DataLayer.get_hotel_byId(Hotel.HotelId).HotelName;

            //save hotel
            DataLayer.add_hotel(Hotel);

            //set a folder for hotel
            string hotel_dir = ctrl.Server.MapPath(@"~\Upload\" + Hotel.HotelName);
            if (Hotel.HotelId == 0)
            {
                if (!Directory.Exists(hotel_dir))
                    Directory.CreateDirectory(hotel_dir);
            }
            //this is edit
            else
            {
                if (original_hotel_name.ToLower() != Hotel.HotelName.ToLower())
                {
                    string pre_hotel_dir = ctrl.Server.MapPath(@"~\Upload\" + original_hotel_name);
                    Directory.Move(pre_hotel_dir, hotel_dir);
                }
            }

            //save image
            if (Hotel.PhotoFile != null)
            {
                HttpPostedFileBase file = Hotel.PhotoFile;
                var filename = file.FileName;
                file.SaveAs(hotel_dir + "\\main.jpg");
            }

        }

        public HotelViewModel GetHotelDescription(int id)
        {
            HotelViewModel vm = DataLayer.get_hotel_byId(id);

            return vm;
        }

        public HotelViewModel Get_DeleteHotel(int id, int? page, string filter)
        {
            HotelViewModel vm = new HotelViewModel();
            vm.HotelId = id;
            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";

            return vm;
        }

        public void Post_DeleteHotel(HotelViewModel Hotel, Controller ctrl)
        {
            string hotelName = DataLayer.get_hotel_byId(Hotel.HotelId).HotelName;

            if (!String.IsNullOrEmpty(hotelName))
            {
                string hotel_dir = ctrl.Server.MapPath(@"~\Upload\" + hotelName);

                DataLayer.delete_hotel(Hotel.HotelId);

                if (Directory.Exists(hotel_dir))
                    Directory.Delete(hotel_dir, true);
            }
        }


        public HotelImagesViewModel Get_HotelImagesView(int id, Controller ctrl)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();
            vm.HotelName = DataLayer.get_hotel_byId(id).HotelName;
            string hotel_dir = ctrl.Server.MapPath(@"~\Upload\" + vm.HotelName);
            if (Directory.Exists(hotel_dir))
                vm.uploaded_images = Directory.GetFiles(hotel_dir).Select(x => Path.GetFileName(x)).Where(x => x != "main.jpg").ToArray();
            vm.HotelId = id;

            return vm;
        }

        public HotelImagesViewModel Get_HotelPhoto(string photoName)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();

            vm.PhotoName = DataLayer.get_hotel_name_by_photo(photoName) + "\\" + photoName;

            return vm;
        }

        public void Post_AddHotelPhoto(HotelImagesViewModel vm, int hotel_ID, Controller ctrl)
        {
            HttpPostedFileBase item = vm.image;

            if (item != null && hotel_ID != 0)
            {
                string file_name = DataLayer.save_hotel_image(hotel_ID);
                if (!String.IsNullOrEmpty(file_name))
                {
                    string hotel_name = file_name.Substring(0, file_name.LastIndexOf('_'));

                    string hotel_dir = ctrl.Server.MapPath(@"~\Upload\" + hotel_name);
                    if (!Directory.Exists(hotel_dir))
                        Directory.CreateDirectory(hotel_dir);

                    item.SaveAs(hotel_dir + "\\" + file_name);
                }
            }

        }
     
        public void Post_DeleteHotelPhoto(HotelImagesViewModel photo, Controller ctrl)
        {
            DataLayer.delete_hotel_image(photo.PhotoName);
            string file_path = ctrl.Server.MapPath(@"\Upload\" + photo.PhotoName.Substring(0, photo.PhotoName.LastIndexOf('_')) + "\\" + photo.PhotoName);
            if (System.IO.File.Exists(file_path))
                System.IO.File.Delete(file_path);
        }


        #endregion Hotel

        #region City

        public IPagedList<CityViewModel> Get_CityList(int? page, string filter)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            List<CityViewModel> lst_cities = DataLayer.get_cities();
            foreach (CityViewModel c in lst_cities)
                if (c.cityAttractions != null)
                    c.cityAttractions = (c.cityAttractions.Length < 100 ? c.cityAttractions : (c.cityAttractions.Substring(0, 100) + "..."));

            if (!String.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                lst_cities = lst_cities.Where(x => x.cityName.ToLower().Contains(filter)).ToList();
            }

            //sort and set row number
            lst_cities = lst_cities.OrderBy(x => x.cityName).Select((x, index) => new CityViewModel
            {
                RowNum = index + 1,
                cityID = x.cityID,
                cityName = x.cityName,
                cityAttractions = x.cityAttractions
            }).ToList();


            IPagedList<CityViewModel> paged_list_city = lst_cities.ToPagedList(currentPageIndex, pageSize);

            return paged_list_city;
        }



        public CityViewModel Get_AddNewCity(int? city_id, int? page, string filter)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = city_id.HasValue ? city_id.Value : 0;

            //this is edit
            if (vm.cityID != 0)
            {
                List<string> city_prop = DataLayer.get_city_byId(vm.cityID);
                vm.cityName = city_prop[0];
                vm.cityAttractions = city_prop[1];
            }

            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";

            return vm;
        }

        public void Post_AddNewCity(CityViewModel city)
        {
            DataLayer.add_city(city.cityID, city.cityName, city.cityAttractions);
        }

        public CityViewModel GetCityDescription(int city_id)
        {
            List<string> city_prop = DataLayer.get_city_byId(city_id);
            CityViewModel vm = new CityViewModel();
            vm.cityName = city_prop[0];
            vm.cityAttractions = city_prop[1];

            return vm;
        }

        public CityViewModel Get_DeleteCity(int city_id, int? page, string filter)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = city_id;
            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";

            return vm;
           
        }

        public void Post_DeleteCity(CityViewModel city)
        {
            DataLayer.delete_city(city.cityID);
        }

        #endregion City

        #region Amenity

        public IPagedList<AmenityViewModel> Get_AmenityList(int? page, string filter)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            List<AmenityViewModel> lst_amenity = DataLayer.get_Amenities();
        

            if (!String.IsNullOrEmpty(filter) && filter != "null")
            {

                filter = filter.ToLower();
                lst_amenity = lst_amenity.Where(x => x.AmenityName.ToLower().Contains(filter)).ToList();
            }

            lst_amenity = lst_amenity.OrderBy(x => x.AmenityName).Select((x, index) => new AmenityViewModel
            {
                RowNum = index + 1,
                AmenityName = x.AmenityName,
                AmenityID = x.AmenityID
            }).ToList();

            IPagedList<AmenityViewModel> paged_list = lst_amenity.ToPagedList(currentPageIndex, pageSize);

            return paged_list;
        }

        public void Post_AddNewAmenity(string amenity_name, string amenity_id, int? page)
        {
            int id = String.IsNullOrEmpty(amenity_id) ? 0 : Int32.Parse(amenity_id.ToString());
            DataLayer.add_amenity(id, amenity_name);          
            
        }

        public AmenityViewModel Get_DeleteAmenity(int amenity_id, int? page, string filter)
        {
            AmenityViewModel vm = new AmenityViewModel();
            vm.AmenityID = amenity_id;
            vm.CurrentFilter = String.IsNullOrEmpty(filter) ? "" : filter.ToString();
            vm.CurrentPage = page.HasValue ? page.Value : 1;

            return vm;
        }

        public void Post_DeleteAmenity(AmenityViewModel amenity)
        {
            DataLayer.delete_Amenity(amenity.AmenityID);
        }

        #endregion Amenity

    }
}