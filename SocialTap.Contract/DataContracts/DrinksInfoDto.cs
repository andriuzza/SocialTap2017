using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTap.Contract.DataContracts
{
    public class DrinksInfoDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
