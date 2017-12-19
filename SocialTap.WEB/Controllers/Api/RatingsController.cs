using Microsoft.AspNet.Identity;
using SocialTap.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialTap.WEB.Controllers.Api
{
    public class RatingsController : ApiController
    {
        private readonly IRatingRepository _repo;
        public RatingsController(IRatingRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IHttpActionResult Post(int LocationId)
        {
            var UserId = User.Identity.GetUserId();
            var post = _repo.PostRating(UserId, LocationId);
            if (post.IsSuccess)
            {
                return Ok(post.Item);
            }
            return BadRequest(post.ErrorMessage);
        }
    }
}
