using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Repositories
{
    public class FeedbackRepository : ISystemRepository<LocationFeedbackDto>
    {
        private readonly ApplicationDbContext _db;
        public FeedbackRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(LocationFeedbackDto item)
        {

            _db.LocationFeedbacks.Add(new Models.Feedbacks.LocationFeedback
            {
                Id = item.Id,
                LocationId = item.LocationId,
                Feedback = item.Feedback,
                User = item.User

            });
            _db.SaveChangesAsync();
        }
    }
}
