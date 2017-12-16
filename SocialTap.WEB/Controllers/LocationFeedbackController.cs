using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
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
        private readonly ApplicationDbContext _db;
        private readonly ISystemRepository<LocationFeedbackDto> _repository;
        private readonly IGeneralData _general;

        public LocationFeedbackController(ApplicationDbContext db, ISystemRepository<LocationFeedbackDto> repository,IGeneralData general)
        {
            _db = new ApplicationDbContext();
            _repository = repository;
            _general = general;
        }

        public ActionResult Index(string place)
        {
            var b = _db.LocationFeedbacks;

            var vieta = _db.Locations.Where(a => a.Name.Equals(place)).FirstOrDefault();

            int placeId = vieta.Id;

            return RedirectToAction("ShowFeedback", new { id = placeId });
            /*

            var viewModel = new FeedbackViewModel
            {
                location = _db.Locations
                .ToList(),
                
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
            */
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
                return View();

            }
            return View(locationFeedback);
        }
    }
}