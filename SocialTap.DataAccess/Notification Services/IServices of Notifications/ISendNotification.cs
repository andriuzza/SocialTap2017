using SocialTap.DataAccess.Models;
using SocialTap.Services.Notification_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Notification_Services.IServices_of_Notifications
{
    public interface ISendNotification
    {
        void FindSubscribers(NotificationHandling handler, string message);
        void InsertNotification(object o, NotificationEventArgs noti);
        NotificationEventArgs CreateNotification(string message);
    }
}

