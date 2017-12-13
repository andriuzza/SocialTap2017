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
                    var number = db.NotificationUsers
                        .Where(a => a.UserAccountId == user.Id)
                        .Count();
                    /*Checks if notifications for one user isn't more or equal to 5 */
                    /*if yes - deletes them until notification number is 4 */
                    /*using >= 'cause in the scenario where request happens almost in the same time it is possible
                     * to have more 5 notifications per user*/
                    if (number >= 5) 
                    {
                        while (db.NotificationUsers.Where(u => u.UserAccountId == user.Id).Count() != 4)
                        {
                            var notifi = db.NotificationUsers
                                .Where(u=>u.UserAccountId == user.Id).ToList()
                                .Last();

                            db.NotificationUsers.Remove(notifi);
                            db.SaveChanges();
                        }
                      


                        NotificationUser ns = new NotificationUser()
                        {
                            UserAccountId = user.Id,
                            NotificationId = notification.Id
                        };
                        db.NotificationUsers.Add(ns);
                    }
                   
                   

                }
                db.SaveChanges();
            }
        }

        public NotificationEventArgs CreateNotification(string message)
        {
            Notification notification = new Notification();
            notification.NotificationText = message; 

            return new NotificationEventArgs()
            {
                Message = message
            };
        }
    }
}
