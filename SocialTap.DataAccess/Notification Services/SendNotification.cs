using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Models.Notifications;
using SocialTap.WEB.Models;
using SocialTap.DataAccess.Notification_Services.IServices_of_Notifications;
using SocialTap.Contract.DataContracts;

namespace SocialTap.Services.Notification_Services
{
    public class SendNotification: ISendNotification
    {
        static ICollection<ApplicationUser> Accounts { get; set; }

        public void FindSubscribers(NotificationHandling handler, string message)
        {
             handler.NewDrink += InsertNotification;
             handler.NewDrinkUploaded(CreateNotification(message));
        }

        public void InsertNotification(object o, NotificationEventArgs noti)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Notification notification = new Notification();
                notification.NotificationText = noti.Message;

                db.Notifications.Add(notification);
                db.SaveChanges();

                var users = db.Users.ToList();
                foreach (var user in users)
                {
                    NotificationUser ns = new NotificationUser()
                    {
                        UserAccountId = user.Id,
                        NotificationId = notification.Id
                    };
                    db.NotificationUsers.Add(ns);

                }
                db.SaveChanges();
            }
        }

        public NotificationEventArgs CreateNotification(string message)
        {
            Notification notification = new Notification();
            notification.NotificationText = message; /*not yet sure if it is needed here... */

            return new NotificationEventArgs()
            {
                Message = message
            };
        }
    }
}
