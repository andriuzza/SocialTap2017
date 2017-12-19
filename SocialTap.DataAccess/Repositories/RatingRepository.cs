using SocialTap.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using SocialTap.WEB.Models;
using SocialTap.DataAccess.Models.RatingSystem;

namespace SocialTap.DataAccess.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _db;
        public RatingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public CommonResult<int> PostRating(string UserId, int LocationId)
        {
            var user = _db.Users
                .Where(a => a.Id.Equals(UserId))
                .Any();

            if (user == false)
            {
                return CommonResult<int>
                    .Failure<int>("The user doesn't exist");
            }

            var location = _db.Locations
                .Where(a => a.Id == LocationId)
                .Any();

            if(location == false)
            {
                return CommonResult<int>
                    .Failure<int>("The location doesn't exist");
            }

            var userRating = _db.UserRatings
                .Where(a => a.LocationId == LocationId
                && a.UserAccountId.Equals(UserId)).FirstOrDefault();

            var locationPoints = _db.Locations.Where(a => a.Id == LocationId)
                      .Select(a => a.RatingOfCapture)
                      .First();

            if (userRating == null)
            {
                try
                {
                   

                    if (userRating == null)
                    {
                        _db.UserRatings.Add(new UserRating
                        {
                            LocationId = LocationId,
                            UserAccountId = UserId,
                            Rating = locationPoints
                        });

                        _db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    return CommonResult<int>
                       .Failure<int>("Something wrong db" + ex.Message);
                }
            }
            else
            {
                userRating.Rating += locationPoints;
                _db.SaveChanges();
            }
           var pointOfUserNow = _db.UserRatings.Where(a => a.Id == LocationId && a.LocationId == LocationId)
                                 .Select(a => a.Rating)
                                 .First();
            return CommonResult<int>.Success(pointOfUserNow);






        }
    }
}
