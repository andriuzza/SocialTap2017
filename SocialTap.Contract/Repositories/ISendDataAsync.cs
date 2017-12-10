using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.Repositories
{
   public interface ISendDataAsync
    {
       Task<CommonResult<DrinkRatingDto>> SendRatingAsync(DrinkRatingDto rating);
       CommonResult<DrinkEditDto> Edit(int? id);
    }
}
