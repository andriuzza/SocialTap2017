using System.Web.Mvc;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.Web.ViewModels;
using SocialTap.WEB.Models;
using System.Linq;
using SocialTap.WEB.ViewModels;
using SocialTap.Contract.DataContracts;

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
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                var ViewRating = new RatingViewModel
                {
                    Locations = _db.Locations.Select(a => new Contract.DataContracts.Location
                    {
                        Name = a.Name,
                        Id = a.Id
                    }).ToList()
                };

                return View(ViewRating);
            }
        }

        [HttpPost]
        public ActionResult Index(RatingViewModel ViewRating)
        {
            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                DataAccess.Models.Location FoundLocation = _db.Locations
                    .Where(a => a.Id == ViewRating.LocationId
                    && ViewRating.Rating != 0).FirstOrDefault();

                if(FoundLocation != null)
                {
                    return RedirectToAction("InsertRating", "Drinks", ViewRating);
                }
                return View();
            }
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