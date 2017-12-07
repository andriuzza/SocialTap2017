using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Models.Notifications;

namespace SocialTap.Services.Notification_Services
{

    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
