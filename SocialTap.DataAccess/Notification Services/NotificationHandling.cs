using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.DataAccess.Models;

namespace SocialTap.Services.Notification_Services
{
    public class NotificationHandling
    {
        public delegate void NewDrinkEventHandler(object o, NotificationEventArgs args);

        public event NewDrinkEventHandler NewDrink;

        public Drink LocalDrink { get; }

        public NotificationHandling()
        {
        }

        public NotificationHandling(Drink Drink)
        {
            LocalDrink = Drink;
            SendNotification.FindSubscribers(this, LocalDrink);
        }

        public virtual void NewDrinkUploaded(NotificationEventArgs noti)
        {
            NewDrink?.Invoke(this, noti);
        }
    }

}