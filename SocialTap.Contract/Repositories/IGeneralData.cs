using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;

namespace SocialTap.Contract.Repositories
{
    public interface IGeneralData
    {
        CommonResult<IEnumerable<LocationFormDto>> ShowBarsInformaiton(string sortOrder, string searchString = null);
        CommonResult<IQueryable<DrinksInfoDto>> GetDrinksList();
    }
}
