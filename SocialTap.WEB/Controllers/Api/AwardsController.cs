using Microsoft.AspNet.Identity;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialTap.WEB.Controllers.Api
{
    public class AwardsController : ApiController
    {
        private readonly ApplicationDbContext _db;
        public AwardsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IHttpActionResult GetAward(string AwardName)
        {
            var getProduct = _db.Awards
                .Where(a => a.Name.Equals(AwardName))
                .Select(b => new
                {   Id = b.Id,
                    LocationId = b.LocationId,
                    Points = b.RequiredPoints
                }
                ).First();

            var UserId = User.Identity.GetUserId();
            var UserPubPoints = _db.UserRatings
                .Where(a => a.LocationId == getProduct.LocationId
                && a.UserAccountId.Equals(UserId)).First();

            if(getProduct.Points <= UserPubPoints.Rating)
            {
                UserPubPoints.Rating = UserPubPoints.Rating - getProduct.Points;
                _db.SaveChanges();
                _db.UserAwards.Add(new UserAward
                {
                    AwardId = getProduct.Id,
                    ApplicationUserId = UserId
                });
                _db.SaveChanges();

               
                return Ok();
            }

            return BadRequest();

                
        }
    }
}
