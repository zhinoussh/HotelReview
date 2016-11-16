using HotelAdvice.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelAdvice.Controllers
{
    public class BaseController : Controller
    {
        private IServiceLayer _service;

        protected BaseController(IServiceLayer service)
        {
            _service = service;
        }

        protected IServiceLayer DataService
        { 
            get{
                return _service;
            }
        }


    }
}