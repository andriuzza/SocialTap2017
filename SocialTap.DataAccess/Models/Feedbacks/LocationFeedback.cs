using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.DataAccess.Models.Feedbacks
{
    public class LocationFeedback
    {
        public int _LocationId;

        public int Id { get; set; }
        public int LocationId
        {
            get { return _LocationId; }
            set { _LocationId = value; }
        }
        public string Feedback { get; set; }
        public string User { get; set; }
    }
}
