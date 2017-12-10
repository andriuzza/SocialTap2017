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
using SocialTap.WEB.ViewModels;
using System.Data.Entity;
using System.Web.Http;
using SocialTap.Services.Services;

namespace SocialTap.Web.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ISystemService<DrinkDto> _service;
        private readonly ISystemRepository<DrinkType> _typeService;
        private readonly ISendDataAsync _data;

        public DrinksController(ApplicationDbContext db, ISystemService<DrinkDto> service,
            ISystemRepository<DrinkType> typeService,
            ISendDataAsync data)
        {
            _db = new ApplicationDbContext();
            _service = service;
            _typeService = typeService;
            _data = data;
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
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Post(DrinkViewModel viewModel)
        {
           viewModel.DrinkTypes = _db.DrinkTypes.ToList();
            viewModel.Locations = from a in _db.Locations
                select new Contract.DataContracts.Location
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
        

        public ActionResult InsertRating(RatingViewModel RatingView)
        {
            ViewBag.Latitude = RatingView.Latitude;
            ViewBag.Longitude = RatingView.Longitude;


            LocationDrinkDto showUserList;
            IEnumerable<DrinkDto> drinks;
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                DataAccess.Models.Location Location = _db.Locations
                    .Where(a => a.Id == RatingView.LocationId)
                    .FirstOrDefault();
                if (Location == null)
                {
                    ViewBag.Message = "No drinks or wrong pub entered";
                    return View();
                }
                drinks = (from a in _db.Drinks
                          where Location.Id == a.LocationOfDrinkId
                          select new DrinkDto
                          {
                              Name = a.Name,
                              Price = a.Price,
                              Id = a.Id,
                              RatingAverage = _db.DrinkRating
                              .Where(b => b.DrinkId == a.Id)
                              .Average(r => r.Rating)
                         }).ToList();

                showUserList = new LocationDrinkDto
                {
                    Address = Location.Address,
                    PubName = Location.Name,
                    Latitude = Location.Latitude,
                    Longitude = Location.Longitude,
                    Rating = RatingView.Rating
                };


            }
            showUserList.Drinks = drinks;
            return View(showUserList);
        }

        public async Task<ActionResult> GetAllDrinks(string sortOrder,
         string currentFilter,
            string searchString,
                 int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var drinks = from s in _db.Drinks
                         select s;

            var result = PagedService<DrinksInfoDto>.GetDrinks(sortOrder, drinks);



            int pageSize = 3;
            return View(await PagedService<Drink>.CreateAsync(result.AsNoTracking(), page ?? 1, pageSize));

        }

        public ActionResult Edit(int id)
        {
            var result = _data.Edit(id);
            if (result.IsSuccess)
            {
                return View(result.Item);
            }

            return Content(result.ErrorMessage);
        }
    
        public async Task<ActionResult>SaveEdit(DrinkEditDto drink)
        {
            var drinkItem = await _db.Drinks.SingleAsync(a => a.Id == drink.Id);
            drinkItem.Name = drink.Name;
            drinkItem.Price = drink.Price;
           if( await _db.SaveChangesAsync() > 0)
            {
                ViewBag.Message = "Success!";
            }
            ViewBag.Message = "Error";
          
            return RedirectToAction("Edit", new { id= drink.Id});
        }
    }
}