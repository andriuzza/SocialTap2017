using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public virtual ICollection<NotificationUser> Notifications { get; set; }
        public Notification()
        {
            Notifications = new Collection<NotificationUser>();
        }
    }
}
