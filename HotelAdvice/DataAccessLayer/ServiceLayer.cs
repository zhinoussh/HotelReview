using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelAdvice.Areas.Admin.ViewModels;
using PagedList;
using HotelAdvice.Areas.Admin.Models;
using System.Web.Mvc;
using System.IO;
using HotelAdvice.Areas.WebSite.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using HotelAdvice.Helper;


namespace HotelAdvice.DataAccessLayer
{
    public class ServiceLayer: IServiceLayer 
    {
        private IDataRepository _dataLayer;
        const int pageSize = 10;
        const int defaultPageSize_userpage = 3;
        const int defaultPageSize_searchpage = 4;
        const int defaultPageSize_reviewpage = 4;
        const int defaultPageSize_HomePage = 9;

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

        public HomeViewModel Get_HomePage(int?page) {

            int currentPageIndex = page.HasValue ? page.Value : 1;

            HomeViewModel vm = new HomeViewModel();
            vm.lst_city = DataLayer.get_cities().OrderBy(x => x.cityName).ToPagedList<CityViewModel>(currentPageIndex, defaultPageSize_HomePage);
            vm.lst_poupular_hotels = DataLayer.Search_Popular_Hotels().ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_HomePage);
            vm.lst_top_hotels = DataLayer.Search_Top_Hotels().ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_HomePage);

            //set advanced search view model
            vm.Advanced_Search = Set_Advanced_Search_Fields("", null, null, null, "", null, null, null, null, null, "");

            return vm;
        }

        public AdvancedSearchViewModel Set_Advanced_Search_Fields(string HotelName, int? cityId
            , int? center, int? airport, string score, bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string selected_amenities)
        {
            AdvancedSearchViewModel vm_search = new AdvancedSearchViewModel();

            List<CityViewModel> lst_city = DataLayer.get_cities().OrderBy(x => x.cityName).ToList();
            List<CityViewModel> search_city = lst_city;
            search_city.Add(new CityViewModel { cityID = 0, cityName = "Select City" });
            vm_search.City_List = new SelectList(search_city, "cityID", "cityName");
            vm_search.selected_city = cityId.HasValue ? cityId.Value : 0;

            vm_search.Hotel_Name = HotelName;
            vm_search.Guest_Rating = score;
            vm_search.Star1 = Star1.HasValue ? Star1.Value : false;
            vm_search.Star2 = Star2.HasValue ? Star2.Value : false;
            vm_search.Star3 = Star3.HasValue ? Star3.Value : false;
            vm_search.Star4 = Star4.HasValue ? Star4.Value : false;
            vm_search.Star5 = Star5.HasValue ? Star5.Value : false;

            if (!String.IsNullOrEmpty(score))
            {
                string[] temp = score.Split(new char[] { ',' });
                vm_search.Min_Guest_Rating = float.Parse(temp[0]);
                vm_search.Max_Guest_Rating = float.Parse(temp[1]);
                vm_search.Guest_Rating = score;
            }
            else
            {
                vm_search.Min_Guest_Rating = 0;
                vm_search.Max_Guest_Rating = 5;
            }

            string stars = "";
            if (Star1 == true)
                stars += "1,";

            if (Star2 == true)
                stars += "2,";

            if (Star3 == true)
                stars += "3,";

            if (Star4 == true)
                stars += "4,";

            if (Star5 == true)
                stars += "5,";

            if (stars.Length > 0)
                stars = stars.Remove(stars.Length - 1, 1);

            vm_search.hotel_stars = stars;


            List<KeyValuePair<int, string>> lst_locations = new List<KeyValuePair<int, string>>();
            lst_locations.Add(new KeyValuePair<int, string>(1000, "Any Kilometer"));
            lst_locations.Add(new KeyValuePair<int, string>(1, "less than 1 km"));
            lst_locations.Add(new KeyValuePair<int, string>(2, "less than 2 km"));
            lst_locations.Add(new KeyValuePair<int, string>(3, "less than 3 km"));
            lst_locations.Add(new KeyValuePair<int, string>(4, "less than 4 km"));
            lst_locations.Add(new KeyValuePair<int, string>(5, "less than 5 km"));
            lst_locations.Add(new KeyValuePair<int, string>(10,"less than 10 km"));
            lst_locations.Add(new KeyValuePair<int, string>(20,"less than 20 km"));
            vm_search.Location = new SelectList(lst_locations, "Key", "Value");
            vm_search.distance_airport = airport.HasValue ? airport.Value : 1000;
            vm_search.distance_city_center = center.HasValue ? center.Value : 1000;
            

            vm_search.lst_amenity = DataLayer.get_Amenities_For_search(selected_amenities);

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
                if (File.Exists(ctrl.Server.MapPath(@path)))
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

        public HotelImagesViewModel Get_DeleteHotelPhoto(string photo_name)
        {
            HotelImagesViewModel vm = new HotelImagesViewModel();
            vm.PhotoName = photo_name;

            return vm;
        }

        public void Post_DeleteHotelPhoto(HotelImagesViewModel photo, Controller ctrl)
        {
            DataLayer.delete_hotel_image(photo.PhotoName);
            string file_path = ctrl.Server.MapPath(@"\Upload\" + photo.PhotoName.Substring(0, photo.PhotoName.LastIndexOf('_')) + "\\" + photo.PhotoName);
            if (File.Exists(file_path))
                File.Delete(file_path);
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
        
        public CityViewModel Get_AddNewCity(Controller ctrl, int? city_id, int? page, string filter)
        {
            CityViewModel vm = new CityViewModel();
            vm.cityID = city_id.HasValue ? city_id.Value : 0;
            vm.imgPath = "/images/empty.gif?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
           
            //this is edit
            if (vm.cityID != 0)
            {
                List<string> city_prop = DataLayer.get_city_byId(vm.cityID);
                vm.cityName = city_prop[0];
                vm.cityAttractions = city_prop[1];

                string path = "/Upload/City/" + vm.cityName + ".jpg";
                if (File.Exists(ctrl.Server.MapPath(@path)))
                    vm.imgPath = path + "?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
                else
                    vm.imgPath = "/images/empty.gif?" + DateTime.Now.ToString("ddMMyyyyhhmmsstt");
            }

            vm.CurrentPage = page.HasValue ? page.Value : 1;
            vm.CurrentFilter = !String.IsNullOrEmpty(filter) ? filter.ToString() : "";

            return vm;
        }

        public void Post_AddNewCity(CityViewModel city, Controller ctrl)
        {
            string original_city_name = "";
            if (city.cityID != 0)
                original_city_name = DataLayer.get_city_byId(city.cityID)[0];

            DataLayer.add_city(city.cityID, city.cityName, city.cityAttractions);

            //save image
            string new_img_path = ctrl.Server.MapPath(@"~\Upload\City\" + city.cityName + ".jpg");

            //this is edit
            if (city.cityID != 0)
            {
                string old_img_path = ctrl.Server.MapPath(@"~\Upload\City\" + original_city_name + ".jpg");

                if (File.Exists(old_img_path) && original_city_name.ToLower() != city.cityName.ToLower())
                    File.Move(old_img_path, new_img_path);
            }
            
            if (city.PhotoFile != null)
            {
                HttpPostedFileBase file = city.PhotoFile;
                var filename = file.FileName;
                file.SaveAs(new_img_path);
            }
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

        public void Post_DeleteCity(Controller ctrl, CityViewModel city)
        {
            List<string> city_prop = DataLayer.get_city_byId(city.cityID);
            if (city_prop != null)
                city.cityName = city_prop[0];

            DataLayer.delete_city(city.cityID);

            string img_path = ctrl.Server.MapPath(@"~\Upload\City\" + city.cityName + ".jpg");
            if (File.Exists(img_path))
                File.Delete(img_path);
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


        #region UserPage

        public UserPageViewModel Get_UserProfilePage(string user_id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            UserPageViewModel vm = new UserPageViewModel();

            vm.lst_wishList = (DataLayer.get_wishList(user_id)).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
            vm.lst_rating = (DataLayer.get_ratingList(user_id)).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
            vm.lst_reviews = (DataLayer.get_reviewList(user_id)).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);

            return vm;
        }

        public ReviewPageViewModel Get_ReviewPage(string user_id, Controller ctrl, int hotel_id, int? page)
        {
            ReviewPageViewModel vm = DataLayer.get_review_page(hotel_id);

            AddReviewViewModel your_review = DataLayer.get_previous_review(hotel_id, user_id);

            //this is new review
            if (your_review == null)
            {
                your_review = new AddReviewViewModel();
                your_review.RateId = 0;
                your_review.HotelId = hotel_id;
                your_review.UserId = user_id;
            }
            vm.YourReview = your_review;

            int current_page_index = page.HasValue ? page.Value : 1;

            ApplicationUserManager userMgr = ctrl.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            vm.lst_reviews = DataLayer.get_reviews_for_hotel(hotel_id, userMgr).ToPagedList(current_page_index, defaultPageSize_reviewpage);

            return vm;
        }

        public void Post_AddNewReview(AddReviewViewModel review)
        {
            DataLayer.add_review(review);
        }

        public string Get_PartialReviewList(AddReviewViewModel review, string user_id, Controller ctrl)
        {
            IPagedList<HotelSearchViewModel> model = DataLayer.get_reviewList(user_id).ToPagedList<HotelSearchViewModel>(review.currentPageIndex, defaultPageSize_userpage);
            string partialview = RenderPartial.RenderRazorViewToString(ctrl
                 , "~/Areas/WebSite/views/User/_PartialYourReviewList.cshtml"
                 , model);

            return partialview;

        }

        public AddReviewViewModel Get_EditReview(string user_id, int hotel_id, int? page)
        {
            AddReviewViewModel vm = DataLayer.get_previous_review(hotel_id, user_id);
            vm.fromProfilePage = true;
            vm.currentPageIndex = page.HasValue ? page.Value : 1;

            return vm;

        }

        public DeleteReviewViewModel Get_DeleteReview(string user_id, int hotel_id, int? page)
        {
            DeleteReviewViewModel vm = new DeleteReviewViewModel();

            vm.hotelId = hotel_id;
            vm.currentPageIndex = page.HasValue ? page.Value : 1;
            vm.HotelName = DataLayer.get_hotel_byId(hotel_id).HotelName;
            vm.UserId = user_id;

            return vm;
        }

        public IPagedList<HotelSearchViewModel> Post_DeleteReview(DeleteReviewViewModel review, string user_id)
        {
            DataLayer.delete_review(review.hotelId, review.UserId);
            int currentPageIndex = review.currentPageIndex;

            List<HotelSearchViewModel> lst_reviews = DataLayer.get_reviewList(user_id);
            if (currentPageIndex > 1 && lst_reviews.Count() < currentPageIndex * defaultPageSize_userpage)
                currentPageIndex = currentPageIndex - 1;

            IPagedList<HotelSearchViewModel> paged_list = lst_reviews.ToPagedList(currentPageIndex, defaultPageSize_userpage);

            return paged_list;
            
        }

        public void Post_RateHotel(string user_id, int your_rating,int hotel_id)
        {
            DataLayer.rate_hotel(hotel_id, user_id, your_rating);
        }

        public string Get_PartialRatingList(Controller ctrl,string user_id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            IPagedList<HotelSearchViewModel> model = DataLayer.get_ratingList(user_id).ToPagedList<HotelSearchViewModel>(currentPageIndex, defaultPageSize_userpage);
            string partialview_rating = RenderPartial.RenderRazorViewToString(ctrl
                 , "~/Areas/WebSite/views/User/_PartialRatingList.cshtml"
                 , model);

            return partialview_rating;
        }


        public string[] Post_AddToFavorite(string user_id, Controller ctrl, int hotel_id, int? city_id, int? page, string sort, string HotelName, int? center, int? airport, string score
                                            , bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5,string amenity)
        {
            int result = DataLayer.add_favorite_hotel(hotel_id,user_id);
         
            string msg="";
            if (result == 1)
                msg = "add_favorite_success";
            else
                msg = "favorite_already_exist";

            IPagedList<HotelSearchViewModel> model = Get_PartialHotelResults(user_id, city_id, page, sort, HotelName, center, airport, score, Star1, Star2, Star3, Star4, Star5, amenity);
            string partialview_hotels = RenderPartial.RenderRazorViewToString(ctrl
                , "~/Areas/WebSite/views/SearchHotel/_PartialHotelListResults.cshtml"
                , model);

            return new string[] { msg, partialview_hotels };

        }

        public string Post_AddToFavorite_Detail(string user_id, int hotel_id)
        {
            int result = DataLayer.add_favorite_hotel(hotel_id, user_id);

            string msg = "";
            if (result == 1)
                msg = "add_favorite_success";
            else
                msg = "favorite_already_exist";

            return msg;

        }

        public IPagedList<HotelSearchViewModel> Post_DeleteFavorite(string user_id, int hotel_id, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            DataLayer.remove_favorite_hotel(hotel_id, user_id);

            List<HotelSearchViewModel> lst_wishlist = DataLayer.get_wishList(user_id);
            if (currentPageIndex > 1 && lst_wishlist.Count() < currentPageIndex * defaultPageSize_userpage)
                currentPageIndex = currentPageIndex - 1;

            IPagedList<HotelSearchViewModel> wish_list = lst_wishlist.ToPagedList(currentPageIndex, defaultPageSize_userpage);
            
            return wish_list;
        }

        #endregion UserPage

        #region SearchPage

        public HotelDetailViewModel Get_HotelDetails(string user_id, int hotel_id)
        {
            HotelDetailViewModel vm = DataLayer.get_hoteldetails(hotel_id, user_id);
            return vm;
        }

        public SearchPageViewModel Get_SearchResults(string user_id, bool? citySearch, string HotelName, int? cityId
            , int? center, int? airport, string score, bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string amenity)
        {
            SearchPageViewModel vm = new SearchPageViewModel();
           
            vm.Advnaced_Search = Set_Advanced_Search_Fields(HotelName,cityId,center,airport,score,Star1, Star2, Star3, Star4,Star5, amenity);

            if (citySearch.HasValue && citySearch.Value == true)
            {
                //get hotel list in this city_id
                List<HotelSearchViewModel> lst_hotels = DataLayer.Search_Hotels_in_city(cityId.Value, user_id);

                vm.paged_list_hotels = lst_hotels.ToPagedList(1, defaultPageSize_searchpage);
            }
            else
            {
                //get hotel list by these criterias
                List<HotelSearchViewModel> lst_hotels = DataLayer.Advanced_Search(vm.Advnaced_Search, user_id);

                vm.paged_list_hotels = lst_hotels.ToPagedList(1, defaultPageSize_searchpage);

            }

            return vm;
        }

        public IPagedList<HotelSearchViewModel> Get_PartialHotelResults(string user_id, int? cityId, int? page, string sort, string HotelName, int? center
            , int? airport, string score, bool? Star1, bool? Star2, bool? Star3, bool? Star4, bool? Star5, string amenity)
        {
            AdvancedSearchViewModel vm = Set_Advanced_Search_Fields(HotelName, cityId, center, airport, score, Star1, Star2, Star3, Star4, Star5, amenity);
            
            List<HotelSearchViewModel> lst_hotels = DataLayer.Advanced_Search(vm, user_id);

            //sort
            switch (sort)
            {
                case "distance":
                    lst_hotels = lst_hotels.OrderBy(x => x.distance_citycenter).ToList();
                    break;

                case "rating":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.GuestRating).ToList();
                    break;
                case "5to1":
                    lst_hotels = lst_hotels.OrderByDescending(x => x.HotelStars).ToList();
                    break;
                case "1to5":
                    lst_hotels = lst_hotels.OrderBy(x => x.HotelStars).ToList();
                    break;
                default:
                    break;

            }

            //pagination
            int currentPageIndex = page.HasValue ? page.Value : 1;
            return lst_hotels.ToPagedList(currentPageIndex, defaultPageSize_searchpage);
        }

        #endregion SearchPage

    }
}