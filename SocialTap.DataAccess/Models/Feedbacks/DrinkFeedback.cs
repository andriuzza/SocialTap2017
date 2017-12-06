using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models.Feedbacks
{
    public class DrinkFeedback
    {
        public int Id { get; set; }
        Drink Drink { get; set; }
        public int Rating { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public int LocationId { get; set; }
    }
}
