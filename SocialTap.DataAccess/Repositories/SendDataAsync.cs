using SocialTap.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using SocialTap.WEB.Models;
using SocialTap.DataAccess.Models;
using System.Data.Entity;

namespace SocialTap.DataAccess.Repositories
{
 
    public class SendDataAsync : ISendDataAsync
    {
        private readonly ApplicationDbContext _db;

        public SendDataAsync(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CommonResult<DrinkRatingDto>> SendRatingAsync(DrinkRatingDto rating)
        {
            if (rating.DrinkId == 0)
            {
                return CommonResult<DrinkRatingDto>.Failure<DrinkRatingDto>("Drink id is 0");
            }
            if(rating.Rating == 0)
            {
                return CommonResult<DrinkRatingDto>.Failure<DrinkRatingDto>("Drink rating is 0 (not allowed)");
            }

            bool RatingSuccess = await SaveChanges(rating);

            if (RatingSuccess == true)
            {
                return CommonResult<DrinkRating>.Success(rating);
            }

            return CommonResult<DrinkRatingDto>
                .Failure<DrinkRatingDto>
                ("Something unexpected happend with db");

        }

        private async Task<bool> SaveChanges(DrinkRatingDto rating)
        {
            _db.DrinkRating.Add(new DrinkRating
            {
                Rating = rating.Rating,
                DrinkId = rating.DrinkId
            });

            return await _db.SaveChangesAsync() > 0;
        }

        public CommonResult<DrinkEditDto> Edit(int? id)
        {
            var drink = _db.Drinks.Where(s => s.Id == id).FirstOrDefault();
            if(drink == null)
            {
                 return CommonResult<DrinkEditDto>
               .Failure<DrinkEditDto>
               ("No match id..");
            }

            var editDrink = new DrinkEditDto
            {
                Name = drink.Name,
                Price = drink.Price,
                Id = drink.Id,
                Ratings = GetRatings(id)
            };

            return CommonResult<DrinkRating>.Success(editDrink);
        }

        private IQueryable<int> GetRatings(int? id)
        {
            return _db.DrinkRating
                .Where(a => a.DrinkId == id)
                .Select(a => a.Rating);
        }
    }
}
