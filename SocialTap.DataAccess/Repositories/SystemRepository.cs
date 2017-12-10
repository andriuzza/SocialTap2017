using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;
using PagedList;

namespace SocialTap.DataAccess.Repositories
{
    public class SystemRepository : ISystemRepository<DrinkDto>
    {
        private readonly ApplicationDbContext _db;
        public SystemRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(DrinkDto item)
        {

            _db.Drinks.Add(new Drink
            {
                Name = item.Name,
                Price = item.Price,
                LocationOfDrinkId = item.LocationOfDrinkId,
                DrinkTypeId = item.DrinkTypeId
            });
            _db.SaveChangesAsync();
        }

    }
}
