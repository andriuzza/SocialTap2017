using SocialTap.Contract.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialTap.WEB.ViewModels
{
    public class RatingViewModel
    {
        [Required]
        public int LocationId { get; set; }
        public ICollection<Location> Locations { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The numbers does not fit in boundaries(0 to 100) ")]
        public int Rating { get; set; }

        public float Latitude { get; set; }
        public float  Longitude {get;set;}

        public RatingViewModel()
        {
            Locations = new Collection<Location>();
        }
    }
}