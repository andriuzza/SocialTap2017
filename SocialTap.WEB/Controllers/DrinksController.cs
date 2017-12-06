using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.Contract.Services;
using SocialTap.DataAccess;
using SocialTap.DataAccess.Models;
using SocialTap.Web.ViewModels;
using SocialTap.WEB.Models;
using System.Web.Mvc;
namespace SocialTap.Web.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ISystemService<DrinkDto> _service;
        private readonly ISystemRepository<DrinkType> _typeService;


        public DrinksController(ApplicationDbContext db, ISystemService<DrinkDto> service,
            ISystemRepository<DrinkType> typeService)
        {
            _db = new ApplicationDbContext();
            _service = service;
            _typeService = typeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostDrinkType()
        {
            return View();
        }

        public ActionResult PostDrinkType(DrinkType drink)
        {
            if (ModelState.IsValid)
            {
                _typeService.Add(drink);
                ModelState.Clear();

                return View();
            }

            return View(drink);
        }

        public ActionResult Post()
        {
            var b = _db.DrinkTypes;
            var viewModel = new DrinkViewModel
            {
                DrinkTypes = _db.DrinkTypes.ToList(),
                Locations = from a in _db.Locations
                    select new LocationDto
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
        }

        [HttpPost]
        public ActionResult Post(DrinkViewModel viewModel)
        {
           viewModel.DrinkTypes = _db.DrinkTypes.ToList();
            viewModel.Locations = from a in _db.Locations
                select new LocationDto
                {
                    Id = a.Id,
                    Name = a.Name
                };

            var validaiton = _service.Add(new DrinkDto
            {
                Name = viewModel.Drink.Name,
                Price = viewModel.Drink.Price,
                LocationOfDrinkId = viewModel.LocationId,
                DrinkTypeId = viewModel.DrinkTypeId

                
            });

            if (validaiton.IsSuccess)
            {
                viewModel.Message = "Success!";
                return View(viewModel);
            }

            viewModel.Message = validaiton.ErrorMessage;
            return View(viewModel);
        }
    
            /*     public ActionResult ShowDrinksList()
             {

             }*/
    }
}