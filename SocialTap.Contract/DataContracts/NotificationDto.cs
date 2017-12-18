using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.DataContracts
{
    public class NotificationDto
    {
        public bool IsRead { get; set; }
        public string Notification { get; set; }
    }
}
