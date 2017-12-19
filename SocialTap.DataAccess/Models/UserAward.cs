using SocialTap.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models
{
    public class UserAward
    {
        public int Id { get; set; }

        [Required]
        public int AwardId { get; set; }
        public LocationAward Award { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
      
    }
}
