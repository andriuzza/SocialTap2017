using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Notification_Services.IServices_of_Notifications;

namespace SocialTap.Services.Notification_Services
{
    public class NotificationHandling
    {
        public delegate void NewDrinkEventHandler(object o, NotificationEventArgs args);

        public event NewDrinkEventHandler NewDrink;

        ISendNotification _notification;
        public NotificationHandling(ISendNotification notification, string message)
        {
            _notification = notification;
            _notification.FindSubscribers(this, message);
        }
        public virtual void NewDrinkUploaded(NotificationEventArgs noti)
        {
            NewDrink?.Invoke(this, noti);
        }
    }

}