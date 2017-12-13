using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models.Feedbacks;
using SocialTap.WEB.Models;
using SocialTap.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialTap.WEB.Controllers
{
    public class LocationFeedbackController : Controller
    {
        private readonly ISystemRepository<LocationFeedbackDto> _repository;
        private readonly IGeneralData _general;
        private readonly ApplicationDbContext _db;

        public LocationFeedbackController(ApplicationDbContext db, ISystemRepository<LocationFeedbackDto> repository,IGeneralData general)
        {
            _db = db;
            _repository = repository;
            _general = general;
        }

        public ActionResult Index()
        {
            //var b = _db.Locations;
            var viewModel = new FeedbackViewModel
            {
                Locations = _db.Locations.ToList(),
                LocationFeedback = new LocationFeedback()

                /*DrinkTypes = _db.DrinkTypes.ToList(),
                Locations = from a in _db.Locations
                            select new Contract.DataContracts.Location
                            {
                                Id = a.Id,
                                Name = a.Name
                            },

                Drink = new Drink()
                */
            };
            try
            {
                return View(viewModel);
            }
            catch (NullReferenceException ex)
            {
                return Content("Null exception" + ex.Data);
            }
            return View();
        }
        
        

        public ActionResult ShowFeedback(int id)
        {
            var result = _general.GetFeedbackList(id);
            if (result.IsSuccess)
            {
                return View(result.Item);
            }else
            {

                return View();
            }

        }

        public ActionResult Post()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Post(LocationFeedbackDto locationFeedback)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(locationFeedback);
                ModelState.Clear();
                //return RedirectToAction("showFeedback", "locationFeedback", new { id = locationFeedback.LocationId });

                return RedirectToAction("showFeedback", "locationFeedback", new { id = 1 });

            }
            return View(locationFeedback);
        }
    }
}