using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialTap.WEB.Controllers.Api
{
    public class DrinksController : ApiController
    {
        private readonly ISendDataAsync _data;
        public DrinksController(ISendDataAsync data)
        {
            _data = data;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostRating([FromBody] DrinkRatingDto rating)
       {
            var result = await _data.SendRatingAsync(rating);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(result.ErrorMessage);
        }
        
    }
}
