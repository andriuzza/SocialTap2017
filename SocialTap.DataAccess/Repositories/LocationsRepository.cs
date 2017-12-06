using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;

namespace SocialTap.DataAccess.Repositories
{
    public class LocationsRepository : ISystemRepository<LocationFormDto>
    {
        private readonly ApplicationDbContext _db;
        public LocationsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(LocationFormDto local)
        {
            _db.Locations.Add(new Location()
            {
                Name = local.Name,
                Latitude = local.Latitude,
                Longitude = local.Longitude,
                Address = local.Address
            });
            _db.SaveChanges();
        }
    }
}
