using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Services;
using SocialTap.DataAccess.ExtensionMethod;

namespace SocialTap.Services.Services
{
    public class SortingService: ISortingService
    {
        private IList<LocationFormDto> info = new List<LocationFormDto>();

        public IEnumerable<LocationFormDto> SortByQuery(IEnumerable<LocationFormDto> info, string Query)
        {
            switch (Query)
            {
                case "Address":
                    info = info.OrderBy(s => s.Address);
                    break;
                case "name_desc":
                    info = info.OrderByDescending(s => s.Name);
                    break;
                case "address_desc":
                    info = info.OrderByDescending(s => s.Address);
                    break;

                default:
                    info = info.OrderBy(s => s.Name);
                    break;
            }
            return info;
        }
        public IEnumerable<LocationFormDto> Mapping(DataTable table, string searchString = "")
        {
            foreach (DataRow row in table.Rows)
            {
                var str = row["Name"].ToString();
                if (str.ContainsStrOrNull(searchString))
                {
                    info.Add(new LocationFormDto()
                    {
                        Name = row["Name"].ToString(),
                        Address = row["Address"].ToString(),
                        Longitude = float.Parse(row["Longitude"].ToString()),
                        Latitude = float.Parse(row["Latitude"].ToString())

                    });
                }
            }
            return info;
        }

    }
}
