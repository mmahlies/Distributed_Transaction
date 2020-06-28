using MedicalAPI.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalAPI.Controllers
{
    public class HomeController : Controller
    {
        private IBase _base1;
    
        public HomeController(IBase base1)
        {
            _base1 = base1;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
