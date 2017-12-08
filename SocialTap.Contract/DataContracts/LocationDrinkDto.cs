using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.DataContracts
{
    public class LocationDrinkDto
    {
        public string PubName { get; set; }
        public string Address { get; set; }


        public float Latitude { get; set; }
        public float Longitude { get; set; }


        public IEnumerable<DrinkDto> Drinks { get;set; }

        public int Rating { get; set; }

    }
}
