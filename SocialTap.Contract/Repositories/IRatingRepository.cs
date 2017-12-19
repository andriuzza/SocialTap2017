using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.Repositories
{
    public interface IRatingRepository
    {
        CommonResult<int> PostRating(string UserId, int LocationId);
    }
}
