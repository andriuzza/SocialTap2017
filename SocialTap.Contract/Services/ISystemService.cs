using System;
using System.Collections.Generic;
using System.Text;
using SocialTap.Contract.Common;

namespace SocialTap.Contract.Services
{
    public interface ISystemService<T> where T : class
    {
        CommonResult Add(T item);
    }
}
