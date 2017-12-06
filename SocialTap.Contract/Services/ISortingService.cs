using SocialTap.Contract.DataContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialTap.Contract.Services
{
    public interface ISortingService
    {
        IEnumerable<LocationFormDto> SortByQuery(IEnumerable<LocationFormDto> info
            , string Query);
        IEnumerable<LocationFormDto> Mapping(DataTable table, string searchString = "");
    }
}
