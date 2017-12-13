//using SocialTap.Contract.DataContracts;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Models.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialTap.WEB.ViewModels
{
    public class FeedbackViewModel
    {
        public IEnumerable<LocationFeedback> LocationFeedbacks { get; set; } 

        //public LocationFeedbackDto LocationFeedback { get; set; }
        public Location Location { get; set; }

        public IEnumerable<Location> Locations { get; set; }
        public LocationFeedback LocationFeedback { get; set; }



    }
}