using SocialTap.Contract.DataContracts;
using SocialTap.DataAccess.Models.RatingSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SocialTap.WEB.ViewModels
{
    public class AwardsRatingViewModel
    {
        public UserLocation UserLocationDetail {get;set;}
        public IQueryable<AwardDto> Awards { get; set; }
    }
}