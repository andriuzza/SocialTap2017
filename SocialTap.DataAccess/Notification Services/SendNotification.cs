using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Models.Notifications;
using SocialTap.WEB.Models;

namespace SocialTap.Services.Notification_Services
{
    public static class SendNotification
    {
        static ICollection<ApplicationUser> Accounts { get; set; }

        public static void FindSubscribers(NotificationHandling handler, Drink drink)
        {
             using (ApplicationDbContext db = new ApplicationDbContext())
            {
               handler.NewDrink += InsertNotification;
             handler.NewDrinkUploaded(CreateNotification(drink));
            }
        }

          private static void InsertNotification(object o, NotificationEventArgs noti)
           {
               using (ApplicationDbContext db = new ApplicationDbContext())
               {
                   Notification notification = noti.Notification;
                   db.Notifications.Add(notification);
   
                   var users = db.Users.ToList();
                   foreach (var user in users)
                   {
                       NotificationUser ns = new NotificationUser()
                       {
                           AccountUserID = user.Id,
                           NotificationId = notification.Id
                       };
                       db.NotificationUsers.Add(ns);
   
                   }
                   db.SaveChanges();
               }
           }
           private static NotificationEventArgs CreateNotification(Drink Drink)
           {
               Notification notification = new Notification();
               using (ApplicationDbContext db = new ApplicationDbContext())
               {
                   notification.Drink = Drink;
               }
               return new NotificationEventArgs()
               {
                   Notification = notification
               };
           }
    }
}
