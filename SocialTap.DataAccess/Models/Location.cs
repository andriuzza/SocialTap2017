using SocialTap.DataAccess.Models.RatingSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class Location
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Address { get; set; }
        
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public ICollection<Drink> Drinks { get; set; }
        public ICollection<LocationAward> Awards { get; set; }
        public IReadOnlyCollection<UserRating> UsersRatings { get; set; }

        public int RatingOfCapture { get; set; }

        public Location()
        {
            Drinks = new Collection<Drink>();
            UsersRatings = new Collection<UserRating>();
            Awards = new Collection<LocationAward>();
        }
    }
}
