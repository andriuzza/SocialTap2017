using SocialTap.Contract.DataContracts;
using SocialTap.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SocialTap.WEB.ViewModels
{
    public class AwardViewModel
    {
        public IEnumerable<DataAccess.Models.Location> Locations { get; set; }
        public LocationAward Award { get; set; }

        public int LocationId { get; set; }
        public AwardViewModel()
        {
            Locations = new Collection<DataAccess.Models.Location>();
        }
    }
}