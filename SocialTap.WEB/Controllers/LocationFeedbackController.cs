﻿using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
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

        public LocationFeedbackController(ISystemRepository<LocationFeedbackDto> repository,IGeneralData general)
        {
            _repository = repository;
            _general = general;
        }

        public ActionResult Index()
        {
            var b = _db.DrinkTypes;
            var viewModel = new DrinkViewModel
            {
                DrinkTypes = _db.DrinkTypes.ToList(),
                Locations = from a in _db.Locations
                            select new Contract.DataContracts.Location
                            {
                                Id = a.Id,
                                Name = a.Name
                            },

                Drink = new Drink()
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