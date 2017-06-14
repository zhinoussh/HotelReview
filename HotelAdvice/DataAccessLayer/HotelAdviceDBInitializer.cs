using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using HotelAdvice.Areas.Account.Models;
using HotelAdvice.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelAdvice.DataAccessLayer
{
    public class HotelAdviceDBInitializer : CreateDatabaseIfNotExists<HotelAdviceDB>
    {
        protected override void Seed(HotelAdviceDB context)
        
        {
            // base.Seed(context);

            #region cities

            List<tbl_City> cities = new List<tbl_City>
            {
                new tbl_City
                {
                    CityName = "Isfahan",
                    CityAttractions =
                        "Isfahan is famous for the Islamic architecture as well as Zayanderood River runs in the middle of city. Isfahan is the historical and cultural capital of Iran,renowned for the abundance of great historical monuments and bridges. Masjed Jame Mosque , located in traditional part of isfahan, as well as Sheikh Lotfolah Mosque and Masjed Shah Mosque, located in Naghshe Jahan Square fascinate every tourist by their unique architecture. In addition traditional bazaar in each side of  Naghshe Jahan Square is the best place for buying souvenirs, carpets and other handcrafts."
                },
                new tbl_City
                {
                    CityName = "Tehran",
                    CityAttractions =
                        "Tehran is the capital of Iran and has the largest population and worst traffic congestion among other cities in Iran. However there are lots of tourist attractions in this city which fascinate every visitor. Right in the heart of the city, the Grand Bazaar is an essential visit for any tourist in Tehran. another bazaar in the northern district of Tajrish is smaller, prettier although prices are higher. There is a colorful market of fresh fruit and vegetables, and some excellent touristy shops selling traditional crafts and kitschy memorabilia. Check out the kebab restaurant in the center of the bazaar. You can enjoy several stunning palaces ,which were originally residential palaces for Qajar and Pahlavi kings, including Golestan Palace, Saadabad Complex and Niavaran Palace.An excellent challenge for mountaineers, trails set off from Darakeh and Velanjak, with tea houses staggered along the way."
                },
                new tbl_City
                {
                    CityName = "Shiraz",
                    CityAttractions =
                        "Shiraz is a city steeped in history and poetry, and should be found at the top of any tourist’s itinerary. Shiraz is located in central part of Iran and is renowned specifically for Persepolis, which was the magnificent ceremonial capital of the ancient Achaemenid Empire, over 2500 years ago. The wonderfully Vakil Bazaar is Shiraz’s main market place and is the place for buying rugs, spices, jewellery, and household goods. Stumbling across tea houses, courtyards, caravanserais. Shiraz is famed for its flowers and gardens and Eram Garden and Afif-abad Garden are magnificent among them. Other tourist attractions of this beautiful city are Tomb of Hafez and Saadi, the most loved and respected poets in the Persian literature"
                },
                new tbl_City
                {
                    CityName = "Yazd",
                    CityAttractions =
                        "Yazd is famous for special architecture of its traditional houses, located in desert area. these houses used a specific system of  air conditioning called wind catchers. You will  learn about underground water supplement system (Yariz or qanat) for which Iranians are well-known. The water reservoirs, ice houses, wind catchers and pigeon towers  make it worthwhile to have a visit to Yazd and explore the uniqueness of this ancient city of Iran.this city is also famous for silk and other fabrics. Tallest wind tower of the world also is located in Dolatabad Garden which was originally the house of Yazd's governor. "
                },
                new tbl_City
                {
                    CityName = "Ahvaz",
                    CityAttractions =
                        "Ahvaz is located in south west of Iran and is usually hot and dry. In ahvaz you can enjoy visiting Karun River as well as White bridge on it. Moin-ol-Toja caravanserai (inn) is located next to the white bridge and in the ancient context of Ahvaz.  In addition there is a region in this city called Lashgar Abad, the capital city of Falafel in Iran. This region is a residential place for Arabian people and Falafel,traditional food, is offering in it."
                },
                new tbl_City
                {
                    CityName = "Gilan ",
                    CityAttractions =
                        "This province is located at the north of Iran and lies along the Caspian Sea. In Gilan you can visit Masoule Village as well as Roudkhan Castle,having antiquity of more than 1000 years. In addition you can enjoy several natural waterfalls and springs in this area. Another eye catching tourist attraction of this region is Anzali Lagoon."
                },
                new tbl_City
                {
                    CityName = "Mazandaran",
                    CityAttractions =
                        "This city Located at the north of iran and lies along the Caspian Sea.It is full of eye catching natural jungles including Abbad Abad Jungle and Sisangan Jungle. In addition you can enjoy several natural waterfalls and springs in this area. Another eye catching tourist attraction of this city is Badab Sourt."
                },
                new tbl_City
                {
                    CityName = "Kerman",
                    CityAttractions =
                        "Kerman is the largest province of Iran and has got several ancient sights to offer. Fabulous architecture, a lot of local culture, Zoroastrian faith and fire temples, traditional handicrafts and beautiful landscape are some of the highlights this province. Shahzadeh Mahan Garden, located at 35 km southeast of Kerman city, is the ninth Iranian garden that has been registered on UNESCO’s World Heritage List.  Ganjali Khan Complex is another tourist attraction of this area."
                },
                new tbl_City
                {
                    CityName = "Mashhad",
                    CityAttractions =
                        "Mashhad is one of the religious cities in iran, which is a holly place for moslems. You can visit Imam Reza Holy Shrine in Mashhad and get impressed by mirror works and Islamic art worked in it. Mashhad is the saffron-centre of the world, and you won’t get a better deal anywhere where else than in the bazaars around Fakaleh Ab square. In addition there are several shopping malls. Also traditional restaurants in Shandiz area serve delicious Kebabs! About 40 kilometres outside of Mashhad is the small town of Tus, which is synonymous with the burial site of Iran’s national poet, Ferdowsi. The author of the epic poem, the Shahnameh."
                },

            };

            cities.ForEach(c => context.tbl_city.AddOrUpdate(x => x.CityName, c));
            context.SaveChanges();

            #endregion cities

            #region Hotels

            List<tbl_Hotel> hotels = new List<tbl_Hotel>
            {
                new tbl_Hotel
                {
                    HotelName = "Dad Hotel",
                    CityId = cities.First(c => c.CityName == "Yazd").CityId,
                    HotelAddress = "No.214, Dahom Farvardin Street, Shahid Beheshti Square,Yazd",
                    Tel = "03536229438",
                    Fax = "03536229449",
                    HotelStars = 4,
                    Website = "http://dadhotel.com/",
                    Description =
                        "It was originally a freight company and 10 year ago it has been renovated and refurbished as a traditional hotel. it has fascinating for tourists as it reminds of a traditional caravansary.",
                    checkin = "2:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 1,
                    distance_airport = 12
                },
                new tbl_Hotel
                {
                    HotelName = "Koroush Hotel",
                    CityId = cities.First(c => c.CityName == "Isfahan").CityId,
                    HotelAddress = "Mellat Blvr., 33Pol bridge, Isfahan",
                    Tel = "(031) 36240230-9",
                    Fax = "(031) 36288830",
                    HotelStars = 5,
                    Email = "info@HotelKowsar.com",
                    Website = "www.HotelKowsar.com",
                    Description =
                        "Established 1991, Renovated 2004. with 194 rooms and a beautiful garden.Across the Zayanderooz river and 33Pol bridge. ",
                    checkin = "2:30 PM",
                    checkout = "12:30 PM",
                    distance_citycenter = 1,
                    distance_airport = 15
                },

                new tbl_Hotel
                {
                    HotelName = "Abbasi Hotel",
                    CityId = cities.First(c => c.CityName == "Isfahan").CityId,
                    HotelAddress = "Isfahan Chaharbagh Abbassi Amadegah Street",
                    Tel = "(031) 32226010-19",
                    Fax = "(031) 32226008",
                    HotelStars = 5,
                    Email = "info@abbasihotel.ir",
                    Website = "www.abbasihotel.ir",
                    Description =
                        "Established 1965, Renovated 2015 with 225 rooms. Luxury Hotel with prestigious rooms and lovely garden.",
                    checkin = "2:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 0.5,
                    distance_airport = 15
                },

                new tbl_Hotel
                {
                    HotelName = "Tourist Hotel",
                    CityId = cities.First(c => c.CityName == "Isfahan").CityId,
                    HotelAddress = "Abbas Abad Street, Chaharbagh abbasi",
                    Tel = "(031) 32204479 ",
                    Fax = "(031) 32201639 ",
                    HotelStars = 3,
                    Email = "info@etouristhotel.com",
                    Website = "www.etouristhotel.com",
                    Description = "Near most of amazing sightseeing places...",
                    checkin = "2:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 0.1,
                    distance_airport = 15
                },

                new tbl_Hotel
                {
                    HotelName = "Aseman Hotel",
                    CityId = cities.First(c => c.CityName == "Isfahan").CityId,
                    HotelAddress = "Pol Felezi, Motahari Street",
                    Tel = "(031) 2354141 ",
                    Fax = "(031) 32338121 ",
                    HotelStars = 4,
                    Email = "info@asemanhotel.ir",
                    Website = "www.asemanhotel.ir",
                    Description = "Enjoy the most beautiful view of  Zayanderood in our rooms...",
                    checkin = "2:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 5,
                    distance_airport = 20
                },

                new tbl_Hotel
                {
                    HotelName = "Safaiyeh Hotel",
                    CityId = cities.First(c => c.CityName == "Yazd").CityId,
                    HotelAddress = "Timsar Fallahi Street, Safaiyeh, Emam Hassan Square, Safaiyeh Hotel",
                    Tel = "03538260210-20",
                    Fax = "03538260233",
                    HotelStars = 5,
                    Email = "Info@safaiyeh.com",
                    Website = "http://safaiyeh.pih.ir/",
                    Description = "Located in the middle of historical place",
                    checkin = "3:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 9,
                    distance_airport = 15
                },

                new tbl_Hotel
                {
                    HotelName = "Moshir Hotel",
                    CityId = cities.First(c => c.CityName == "Yazd").CityId,
                    HotelAddress = "Enghelab Street, Moshir Blvrd.",
                    Tel = "035-35239760",
                    Fax = "",
                    HotelStars = 4,
                    Email = "info@hotelgardenmoshir.com",
                    Website = "http://hotelgardenmoshir.com/",
                    Description =
                        "This hotel was originally the residential place of a finance officer in Yazd in the time of Qajar and is a fascinating and stunning place for visitors. It has a beautiful garden in the middle of hotel building and there are some people dressed as time- honored people.",
                    checkin = "2:00 PM",
                    checkout = "12:00 PM",
                    distance_citycenter = 4,
                    distance_airport = 8
                }
            };

            hotels.ForEach(h => context.tbl_Hotel.AddOrUpdate(x => x.HotelName, h));
            context.SaveChanges();

            #endregion hotels

            #region amenities

            List<tbl_amenity> amenities = new List<tbl_amenity>
            {
                new tbl_amenity {AmenityName = "Pool"},
                new tbl_amenity {AmenityName = "Free WiFi"},
                new tbl_amenity {AmenityName = "Free Breakfast"},
                new tbl_amenity {AmenityName = "Parking"},
                new tbl_amenity {AmenityName = "Laundry Service"},
                new tbl_amenity {AmenityName = "gym"},
                new tbl_amenity {AmenityName = "Spa"},
                new tbl_amenity {AmenityName = "Satelite TV"},
                new tbl_amenity {AmenityName = "Room Service"},
                new tbl_amenity {AmenityName = "Beach"},
                new tbl_amenity {AmenityName = "Airport Transfer"},
                new tbl_amenity {AmenityName = "Jacuzzi"},
                new tbl_amenity {AmenityName = "Gameroom"},
                new tbl_amenity {AmenityName = "Biliard"},
                new tbl_amenity {AmenityName = "Elevator"},
                new tbl_amenity {AmenityName = "Safe Box"}
            };

            amenities.ForEach(a => context.tbl_Amenity.AddOrUpdate(x => x.AmenityName, a));
            context.SaveChanges();

            #endregion amenities

            #region Restaurant

            List<tbl_Restuarant> restaurants = new List<tbl_Restuarant>
            {
                new tbl_Restuarant {RestaurantName = "Traditional Restaurant"},
                new tbl_Restuarant {RestaurantName = "Garden Restaurant"},
                new tbl_Restuarant {RestaurantName = "Traditional Tea House"},
                new tbl_Restuarant {RestaurantName = "Italian Restaurant"},
                new tbl_Restuarant {RestaurantName = "Coffee Shop"},
                new tbl_Restuarant {RestaurantName = "Zarin International Restaurant"},
                new tbl_Restuarant {RestaurantName = "Cheheclsotoun International and Iranian restaurant"},
                new tbl_Restuarant {RestaurantName = "Iranian and International Restaurant"},
                new tbl_Restuarant {RestaurantName = "FastFood Retaurant"},
                new tbl_Restuarant {RestaurantName = "Alma Coffee Shop"},
                new tbl_Restuarant {RestaurantName = "Charsoogh Restaurant"},
                new tbl_Restuarant {RestaurantName = "Traditional Baam (Roof) Retaurant"},
                new tbl_Restuarant {RestaurantName = "Traditional and Iranian Mirror Restaurant"}
            };

            restaurants.ForEach(r => context.tbl_Restaurant.AddOrUpdate(x => x.RestaurantName, r));
            context.SaveChanges();

            #endregion Restaurant

            #region Rooms

            List<tbl_room_type> rooms = new List<tbl_room_type>
            {
                new tbl_room_type {Room_Type = "Double Room"},
                new tbl_room_type {Room_Type = "Single Room"},
                new tbl_room_type {Room_Type = "Double Garden View"},
                new tbl_room_type {Room_Type = "Garden suite"},
                new tbl_room_type {Room_Type = "Luxury Suite"},
                new tbl_room_type {Room_Type = "Single Bed"},
                new tbl_room_type {Room_Type = "Normal Suite"},
                new tbl_room_type {Room_Type = "Royal Suite"},
                new tbl_room_type {Room_Type = "Duplex Suite"},
                new tbl_room_type {Room_Type = "Twin Bed"},
                new tbl_room_type {Room_Type = "Ghajar Traditional Suit"},
                new tbl_room_type {Room_Type = "Safavi traditional Suite"},
                new tbl_room_type {Room_Type = "3 Bedrooms Apartment"},
                new tbl_room_type {Room_Type = "2 Bedrooms Apartment"},
                new tbl_room_type {Room_Type = "Pardis suite"},
                new tbl_room_type {Room_Type = "Double Room with extra Bed"},
                new tbl_room_type {Room_Type = "Suite"},
                new tbl_room_type {Room_Type = "Double bed"},
                new tbl_room_type {Room_Type = "Triple Bed"},
                new tbl_room_type {Room_Type = "4 Bed room"}
            };

            rooms.ForEach(r => context.tbl_Room_Type.AddOrUpdate(x => x.Room_Type, r));
            context.SaveChanges();

            #endregion Rooms

            #region sightSeeing

            List<tbl_sightseeing> sightseeings = new List<tbl_sightseeing>
            {
                new tbl_sightseeing {Sightseeing_Type = "Chaharbagh Abbasi"},
                new tbl_sightseeing {Sightseeing_Type = "Historical Bridges(33Pol)"},
                new tbl_sightseeing {Sightseeing_Type = "Chaharbagh Street"},
                new tbl_sightseeing {Sightseeing_Type = "Chaharbagh School"},
                new tbl_sightseeing {Sightseeing_Type = "Zayanderood River"},
                new tbl_sightseeing {Sightseeing_Type = "Aliqapu Palace"},
                new tbl_sightseeing {Sightseeing_Type = "Naghshe Jahan Square"},
                new tbl_sightseeing {Sightseeing_Type = "Traditional Bazare"},
                new tbl_sightseeing {Sightseeing_Type = "Chehelsotoon palace"},
                new tbl_sightseeing {Sightseeing_Type = "Atashgah Mountain"},
                new tbl_sightseeing {Sightseeing_Type = "Monarjonban Historical Monument"},
                new tbl_sightseeing {Sightseeing_Type = "Marnan Historical Bridge"},
                new tbl_sightseeing {Sightseeing_Type = "Dakhme (Zoroastrian Tower of Silence)"},
                new tbl_sightseeing {Sightseeing_Type = "Koohestan Park"},
                new tbl_sightseeing {Sightseeing_Type = "Amirchakhmag Traditional Square"},
                new tbl_sightseeing {Sightseeing_Type = "Zoroastrian Fire Temple"},
                new tbl_sightseeing {Sightseeing_Type = "Traditional Water Resevoir"},
                new tbl_sightseeing {Sightseeing_Type = "Dolatabad Garden"},
                new tbl_sightseeing {Sightseeing_Type = "Masjed Jame Mosque"},
                new tbl_sightseeing {Sightseeing_Type = "Historical Sites"}
            };

            sightseeings.ForEach(r => context.tbl_Sightseeing.AddOrUpdate(x => x.Sightseeing_Type, r));
            context.SaveChanges();

            #endregion sightSeeing

            #region hotelAmenities

            List<tbl_hotel_amenity> hotelAmenities = new List<tbl_hotel_amenity>
            {
                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Jacuzzi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Pool").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },


                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Safe Box").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Satelite TV").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Airport Transfer").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },
                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Jacuzzi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Pool").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Safe Box").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Satelite TV").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Spa").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "gym").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Jacuzzi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Safe Box").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Satelite TV").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Safe Box").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Jacuzzi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Pool").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Elevator").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Airport Transfer").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free Breakfast").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Free WiFi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "gym").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Jacuzzi").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Laundry Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Parking").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Pool").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Room Service").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Safe Box").AmenityID
                },

                new tbl_hotel_amenity
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    AmenityID = amenities.Single(a => a.AmenityName == "Satelite TV").AmenityID
                }

            };

            foreach (var hotelAmenity in hotelAmenities)
            {
                var amenity = context.tbl_Hotel_Amenities.SingleOrDefault(
                    x => x.AmenityID == hotelAmenity.AmenityID &&
                         x.HotelID == hotelAmenity.HotelID);
                if (amenity == null)
                {
                    context.tbl_Hotel_Amenities.Add(hotelAmenity);
                }
            }
            context.SaveChanges();

            #endregion hotelAmenities

            #region hotelRestaurants

            List<tbl_Hotel_Restaurants> hotelRestaurants = new List<tbl_Hotel_Restaurants>
            {
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Coffee Shop").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "FastFood Retaurant").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Iranian and International Restaurant")
                        .RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Coffee Shop").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Garden Restaurant").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Restaurant").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Zarin International Restaurant")
                        .RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Alma Coffee Shop").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Charsoogh Restaurant").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Tea House").RestaurantID
                },
                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Coffee Shop").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Iranian and International Restaurant")
                        .RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Restaurant").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Coffee Shop").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    RestaurantID = restaurants
                        .Single(a => a.RestaurantName == "Traditional and Iranian Mirror Restaurant").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Baam (Roof) Retaurant")
                        .RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RestaurantID = restaurants
                        .Single(a => a.RestaurantName == "Cheheclsotoun International and Iranian restaurant")
                        .RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Garden Restaurant").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Restaurant").RestaurantID
                },

                new tbl_Hotel_Restaurants
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RestaurantID = restaurants.Single(a => a.RestaurantName == "Traditional Tea House").RestaurantID
                }

            };


            foreach (var hotelRestaurant in hotelRestaurants)
            {
                var rest = context.tbl_Hotel_Restaurants.SingleOrDefault(
                    x => x.RestaurantID == hotelRestaurant.RestaurantID &&
                         x.HotelID == hotelRestaurant.HotelID);
                if (rest == null)
                {
                    context.tbl_Hotel_Restaurants.Add(hotelRestaurant);
                }
            }
            context.SaveChanges();

            #endregion hotelRestaurants


            #region hotelRoomss

            List<tbl_Hotel_Rooms> hotelRooms = new List<tbl_Hotel_Rooms>
            {
                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "2 Bedrooms Apartment").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double Room").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Royal Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Single Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Duplex Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Normal Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Royal Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Single Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Twin Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Garden suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Single Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double Room").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double Room with extra Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Single Bed").RoomTypeID
                },
                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Suite").RoomTypeID
                },
                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double Room with extra Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "4 Bed room").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Garden suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Single Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Triple Bed").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "2 Bedrooms Apartment").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "3 Bedrooms Apartment").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Double Room").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Garden suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Ghajar Traditional Suit").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Normal Suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Pardis suite").RoomTypeID
                },

                new tbl_Hotel_Rooms
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    RoomTypeID = rooms.Single(a => a.Room_Type == "Safavi traditional Suite").RoomTypeID
                }


            };

            foreach (var hotelRoom in hotelRooms)
            {
                var rest = context.tbl_Hotel_Rooms.SingleOrDefault(
                    x => x.RoomTypeID == hotelRoom.RoomTypeID &&
                         x.HotelID == hotelRoom.HotelID);
                if (rest == null)
                {
                    context.tbl_Hotel_Rooms.Add(hotelRoom);
                }
            }
            context.SaveChanges();

            #endregion hotelRoomss

            #region sightseeings

            List<tbl_hotel_sightseeing> hotelsights = new List<tbl_hotel_sightseeing>
            {
                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Atashgah Mountain").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Marnan Historical Bridge")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Monarjonban Historical Monument")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Aseman Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Zayanderood River").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh School").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh Street").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chehelsotoon palace").SightseeingID
                },
                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Historical Bridges(33Pol)")
                        .SightseeingID
                },
                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Zayanderood River").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    SightseeingID = sightseeings
                        .Single(a => a.Sightseeing_Type == "Dakhme (Zoroastrian Tower of Silence)").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Koohestan Park").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Aliqapu Palace").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh Abbasi").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh School").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chehelsotoon palace").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Amirchakhmag Traditional Square")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Masjed Jame Mosque").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Traditional Bazare").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Zoroastrian Fire Temple")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Traditional Water Resevoir")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Dolatabad Garden").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Zoroastrian Fire Temple")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Amirchakhmag Traditional Square")
                        .SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Dolatabad Garden").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Historical Sites").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Masjed Jame Mosque").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Traditional Bazare").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Aliqapu Palace").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh Abbasi").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chaharbagh School").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Chehelsotoon palace").SightseeingID
                },

                new tbl_hotel_sightseeing
                {
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId,
                    SightseeingID = sightseeings.Single(a => a.Sightseeing_Type == "Naghshe Jahan Square").SightseeingID
                }
            };

            foreach (var hotelSight in hotelsights)
            {
                var rest = context.tbl_Hotel_Sightseeings.SingleOrDefault(
                    x => x.SightseeingID == hotelSight.SightseeingID &&
                         x.HotelID == hotelSight.HotelID);
                if (rest == null)
                {
                    context.tbl_Hotel_Sightseeings.Add(hotelSight);
                }
            }
            context.SaveChanges();

            #endregion sightseeings


            #region Photo

            List<tbl_Hotel_Photo> photos = new List<tbl_Hotel_Photo>
            {
                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_1.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_4.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_5.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Abbasi_6.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Abbasi Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Koroush (Kowsar)_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Koroush (Kowsar)_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Koroush (Kowsar)_4.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Koroush (Kowsar)_5.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Koroush Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Tourist Hotel_1.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Tourist Hotel_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Tourist Hotel_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Tourist Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Safaiyeh Hotel_1.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Safaiyeh Hotel_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Safaiyeh Hotel_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Safaiyeh Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Daad_1.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Daad_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Daad_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Daad_4.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Dad Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Moshir Hotel_1.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Moshir Hotel_2.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Moshir Hotel_3.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Moshir Hotel_4.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId
                },

                new tbl_Hotel_Photo
                {
                    photo_name = "Moshir Hotel_5.jpg",
                    HotelID = hotels.Single(h => h.HotelName == "Moshir Hotel").HotelId
                }
            };

            //foreach (var photo in photos)
            //{
            //    var img = context.tbl_Hotel_Photo.SingleOrDefault(
            //        x => x.photo_name == photo.photo_name &&
            //             x.HotelID == photo.HotelID);
            //    if (img == null)
            //    {
            //        context.tbl_Hotel_Photo.Add(photo);
            //    }
            //}
            photos.ForEach(p => context.tbl_Hotel_Photo.AddOrUpdate(x => x.photo_name, p));
            context.SaveChanges();

            #endregion Photo

        }
        

    }

    public class IdentityDBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //createRolesandUsers();
        }
      
    }

    


}