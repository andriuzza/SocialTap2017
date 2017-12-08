using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class DrinkRating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }

        public int Rating { get; set; }
    }
}
