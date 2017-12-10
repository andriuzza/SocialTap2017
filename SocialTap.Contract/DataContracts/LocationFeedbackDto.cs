using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.DataContracts
{
    public class LocationFeedbackDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Feedback { get; set; }
        public string User { get; set; }
    }
}
