using SocialTap.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.DataContracts;
using SocialTap.WEB.Models;
using SocialTap.DataAccess.Models;
using System.Data.Entity;

namespace SocialTap.Services.Services
{
        public class PagedService<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PagedService(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);

                AddRange(items);
            }

            public bool HasPreviousPage
            {
                get
                {
                    return (PageIndex > 1);
                }
            }

            public bool HasNextPage
            {
                get
                {
                    return (PageIndex < TotalPages);
                }
            }


            public static async Task<PagedService<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync();
                var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                return new PagedService<T>(items, count, pageIndex, pageSize);
            }

            public static IQueryable<Drink> GetDrinks(string sortOrder, IQueryable<Drink> drinks)
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        drinks = drinks.OrderByDescending(s => s.Name);
                        break;
                    case "Price":
                        drinks = drinks.OrderBy(s => s.Price);
                        break;
                    case "price_desc":
                        drinks = drinks.OrderByDescending(s => s.Price);
                        break;
                    default:
                        drinks = drinks.OrderBy(s => s.Name);
                        break;
                }

                return drinks;
            }
        }
}
