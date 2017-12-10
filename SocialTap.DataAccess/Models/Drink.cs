using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

    
        [Required]
        public int DrinkTypeId { get; set; }
        public DrinkType DrinkType { get; set; }

        [Required]
        public int LocationOfDrinkId { get; set; }
        public Location LocationOfDrink { get; set; }

        public IEnumerable<DrinkRating> Ratings { get; set; }

        public Drink()
        {
            Ratings = new Collection<DrinkRating>();
        }
    }
}