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
                return CommonResult<DrinkRatingDto>.Failure<DrinkRatingDto>("DrinK rating is 0 (not allowed)");
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
    
    }
}
