using System;
using System.Collections.Generic;
using System.Text;
using SocialTap.Contract.Common;

namespace SocialTap.Contract.Repositories
{
    public interface ISystemRepository<T> where T : class
    {
        void Add(T item);
    }
}
