using SocialTap.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models.RatingSystem
{
    public class UserRating
    {
        public int Id { get; set; }

        public string UserAccountId { get; set; }

        public ApplicationUser UserAccount { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int Rating { get; set; }
    }
}
