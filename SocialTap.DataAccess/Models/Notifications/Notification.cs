using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public Drink Drink { get; set; }
        public Location Location { get; set; }
    }
}
