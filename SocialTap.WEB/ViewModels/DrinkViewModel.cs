using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.DataAccess.Models;

namespace SocialTap.Web.ViewModels
{
    public class DrinkViewModel
    {
        public Drink Drink { get; set; }

        public int DrinkTypeId { get; set; }
        public IEnumerable<DrinkType> DrinkTypes { get; set; }

        public int LocationId { get; set; }
        public IEnumerable<LocationDto> Locations { get; set; }

        public string Message { get; set; } = "";

        public DrinkViewModel()
        {
            DrinkTypes = new Collection<DrinkType>();
            Locations = new Collection<LocationDto>();
        }

    }
}
