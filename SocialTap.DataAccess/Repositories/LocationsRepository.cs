using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;
using SocialTap.DataAccess.Notification_Services.IServices_of_Notifications;
using SocialTap.Services.Notification_Services;
using SocialTap.DataAccess.Enums;

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

            NewLocationInserted insert = new NewLocationInserted(local);
            NotificationHandling handler = new NotificationHandling(new SendNotification(), insert.Message);
           

        }
    }
    public class NewLocationInserted
    {
        private LocationFormDto location { get; set;}
        public string Message { get; set; }
        public NewLocationInserted(LocationFormDto _location)
        {
            location = _location;
            Message = Convert();
        }
        public string Convert()
        {
            return "New pub " + location.Name + " appeared in " + location.Address;
        }
    }
}
