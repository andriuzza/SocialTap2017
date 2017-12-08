using SocialTap.Contract.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SocialTap.WEB.ViewModels
{
    public class RatingViewModel
    {

        public int LocationId { get; set; }
        public ICollection<Location> Locations { get; set; }
        public int Rating { get; set; }

        public RatingViewModel()
        {
            Locations = new Collection<Location>();
        }
    }
}