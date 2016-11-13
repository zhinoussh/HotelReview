using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAdvice.Areas.WebSite.ViewModels
{
    public class HotelPhotoAlbumViewModel
    {
        public List<string> photos { get; set; }
        public String HotelName { get; set; }
    }
}