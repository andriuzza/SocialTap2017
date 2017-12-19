using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using Microsoft.AspNet.Identity;
using SocialTap.WEB.Models;

namespace SocialTap.Web.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ISystemRepository<LocationFormDto> _repository;
        private readonly IGeneralData _general;
        private readonly ApplicationDbContext _db;

        public LocationsController(ISystemRepository<LocationFormDto> repository,
            IGeneralData general,
            ApplicationDbContext db)
        {
            _repository = repository;
            _general = general;
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Post()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Post(LocationFormDto location)
        {
           
            if (ModelState.IsValid)
            {
                _repository.Add(location);
                ModelState.Clear();
                ViewBag.Message = "Success!";
                return View();

            }

            return View(location);
        }

        public ActionResult ShowBars(string sortOrder, string searchString = null)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";

            if (searchString != null)
            {
                ViewBag.SearchString = searchString;
            }
            if (ViewBag.SearchString != null)
            {
                searchString = ViewBag.SearchString;
            }

            var validatedData = _general.ShowBarsInformaiton(sortOrder, searchString);

            if (validatedData.IsSuccess)
            {
                ViewBag.Message = "Success";
                return View(validatedData.Item);
            }
            ViewBag.Message = validatedData.ErrorMessage;
            return View();
        }
        public ActionResult ShowPoints()
        {
            var UserId = User.Identity.GetUserId();

            var locationspoints = from a in _db.Locations
                                  join b in _db.UserRatings
                                      on a.Id equals b.LocationId
                                  where b.UserAccountId == UserId
                                  select new UserLocation {
                                      LocationName = a.Name,
                                      UserPoints = b.Rating,
                                      Awards = _db.Awards
                                      .Where(c=>c.LocationId == a.Id)
                                      .Select(x=> new AwardDto {Name = x.Name, RequierdPoints = x.RequiredPoints })
                                      };

            if(locationspoints == null)
            {
                return View();
            }

            return View(locationspoints);
        }
    }
}