using SocialTap.WEB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class LocationAward
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int RequiredPoints { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(14);

        [Required]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<ApplicationUser> UserAccounts { get; set; }

        public LocationAward()
        {
            UserAccounts = new Collection<ApplicationUser>();
        }
    }
}
