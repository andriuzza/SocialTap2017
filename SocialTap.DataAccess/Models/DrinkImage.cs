﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class DrinkImage
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public byte[] ImageOfDrink { get; set; }
    }
}
