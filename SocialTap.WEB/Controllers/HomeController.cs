using System.Web.Mvc;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;
using System.Linq;
using SocialTap.WEB.ViewModels;
using SocialTap.Services.Services.Recognition;
using System.Web;
using System.IO;
using System;

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
        public ActionResult Index(RatingViewModel ViewRating, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid && file == null)
            {
                ViewBag.NoFile = "There is no submited file";
                return View(ViewRating);
            }
                string path = "";
            if (file != null)
            {
                try
                {
                    string pic = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(pic);
                    var uniqueName = string.Format(@"{0}.txt", Guid.NewGuid());
                    path = Path.Combine(Server.MapPath("~/images/profile"), uniqueName + extension);
                    file.SaveAs(path);
                }
                catch (FileLoadException ex)
                {
                    return View("Sorry, Something get wrong with file loading" + ex.Message);
                }
            }

            using (ApplicationDbContext _db = new ApplicationDbContext())
            {
                int rating = Detection.CreateDetection(path);

                ViewRating.Rating = rating;
                DataAccess.Models.Location FoundLocation = _db.Locations
                    .Where(a => a.Id == ViewRating.LocationId
                    && ViewRating.Rating != 0).FirstOrDefault();
                
                if (FoundLocation != null && ViewRating.Rating != 0)
                {
                    return RedirectToAction("InsertRating", "Drinks", ViewRating);
                }

                ViewBag.Error = "Please insert a rating or choose a pub!";
                ViewRating.Locations = _db.Locations.Select(a => new Contract.DataContracts.Location
                {
                    Name = a.Name,
                    Id = a.Id
                })
                .ToList();

                return View(ViewRating);
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