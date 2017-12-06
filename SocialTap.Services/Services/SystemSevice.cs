using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.Contract.Services;

namespace SocialTap.Services.Services
{
    public class SystemService : ISystemService<DrinkDto>
    {
        private readonly ISystemRepository<DrinkDto> _repository;

        public SystemService(ISystemRepository<DrinkDto> repository)
        {
            _repository = repository;
        }

        public CommonResult Add(DrinkDto item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                return CommonResult.Failure("No Name is declared");
            }
            if (item.Price <= 0)
            {
                return CommonResult.Failure("Price is not declared or less or equal then zero!");
            }

            if (item.DrinkTypeId <= 0 || item.LocationOfDrinkId <= 0)
            {
                return CommonResult.Failure("Wrong data with dropdown element");
            }

            _repository.Add(item);

            return CommonResult.Success();
        }
    }
}
