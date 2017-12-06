using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTap.Contract.DataContracts
{
    public class BarsInfo
    {
        public string LocationName { get; set; }
        public string Adress { get; set; }
        public IEnumerable<DrinkDto> Drinks { get; set; }
    }
}
