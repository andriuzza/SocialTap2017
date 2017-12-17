using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;

namespace SocialTap.Web.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ISystemRepository<LocationFormDto> _repository;
        private readonly IGeneralData _general;

        public LocationsController(ISystemRepository<LocationFormDto> repository,
            IGeneralData general)
        {
            _repository = repository;
            _general = general;
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
    }
}