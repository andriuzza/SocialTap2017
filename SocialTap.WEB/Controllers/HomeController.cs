using SocialTap.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;

namespace SocialTap.WEB.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ISystemRepository<DrinkType> _typeService;
        public HomeController(ISystemRepository<DrinkType> typeService)
        {
            _typeService = typeService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}