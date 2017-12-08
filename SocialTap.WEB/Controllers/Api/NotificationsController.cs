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
    public class NotificationsController : ApiController
    {
        private readonly IGeneralData _data;
        public NotificationsController(IGeneralData data)
        {
            _data = data;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
           var UserId=  User.Identity.GetUserId();

           return Ok(_data.GetNotifications(UserId));
        }
    }
}
