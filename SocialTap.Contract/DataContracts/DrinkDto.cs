﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialTap.Contract.DataContracts
{
    public class DrinkDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DrinkTypeId { get; set; }
        public int LocationOfDrinkId { get; set; }
    }
}