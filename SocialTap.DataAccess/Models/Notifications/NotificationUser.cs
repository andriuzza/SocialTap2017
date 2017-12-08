using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.WEB.Models;

namespace SocialTap.DataAccess.Models.Notifications
{
    public class NotificationUser
    {
        [Key]
        [Column(Order = 1)]
        
        public string UserAccountId { get; set; }
        [ForeignKey("UserAccountId")]
        public ApplicationUser UserAccount { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }
        [ForeignKey("NotificationId")]
        public Notification Notification { get; set; }

        public bool IsRead { get; set; }
    }

}
