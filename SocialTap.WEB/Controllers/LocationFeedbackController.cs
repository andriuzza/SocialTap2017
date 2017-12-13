using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.Web.ViewModels;
using SocialTap.WEB.Models;
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

        public LocationFeedbackController(ISystemRepository<LocationFeedbackDto> repository,
            IGeneralData general,
            ApplicationDbContext db)
        {
            _repository = repository;
            _general = general;
            _db = db;
        }

        public ActionResult ShowFeedback()
        {
            var result = _general.GetFeedbackList(2);
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