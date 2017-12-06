using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTap.Contract.DataContracts
{
    public class LocationFormDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
