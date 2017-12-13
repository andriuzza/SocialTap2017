    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.Repositories;
using SocialTap.DataAccess.Models;
using SocialTap.WEB.Models;

namespace SocialTap.DataAccess.Repositories
{
    public class DrinkTypeRepository : ISystemRepository<DrinkType>
    {
        private readonly ApplicationDbContext _db;
        public DrinkTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(DrinkType item)
        {
            _db.DrinkTypes.Add(item);
            _db.SaveChanges();
        }
    }
}
