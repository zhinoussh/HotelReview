using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelAdvice.ViewModels
{
    public class HotelImagesViewModel
    {
        public int HotelId { get; set; }
        public string PhotoName { get; set; }
        public string HotelName { get; set; }

        public HttpPostedFileBase image { get; set; }
        public string[] uploaded_images { get; set; }
    }
}