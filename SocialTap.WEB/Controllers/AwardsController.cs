using SocialTap.WEB.Models;
using SocialTap.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialTap.WEB.Controllers
{
    public class AwardsController : Controller
    {
        // GET: Awards
        private readonly ApplicationDbContext _db;
        public AwardsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostAward()
        {
            var viewModel = new AwardViewModel
            {
                Locations = _db.Locations
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PostAward(AwardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Award.LocationId = viewModel.LocationId;
                _db.Awards.Add(viewModel.Award);
               await _db.SaveChangesAsync();
                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            viewModel.Locations = _db.Locations;
            return View(viewModel);
        }
    }
}