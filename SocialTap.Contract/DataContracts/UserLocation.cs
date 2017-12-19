using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.DataContracts
{
    public class UserLocation
    {
        public string LocationName { get; set; }
        public int UserPoints { get; set; }

        public IEnumerable<AwardDto> Awards { get; set; }

        public UserLocation()
        {
            Awards = new Collection<AwardDto>();
        }
    }
}
