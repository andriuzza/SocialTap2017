using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialTap.Contract.Common;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.Contract.Services;
using SocialTap.DataAccess.ExtensionMethod;
using SocialTap.WEB.Models;
using Microsoft.AspNet.Identity;

namespace SocialTap.DataAccess.Repositories
{
    public class GeneralData : IGeneralData
    {
        private const string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                                                "Initial Catalog=SocialTapDB;" +
                                                "Integrated Security=True";

        private readonly ApplicationDbContext _db;
        private readonly ISortingService _sorting;

        public GeneralData(ApplicationDbContext db, ISortingService sorting)
        {
            _db = db;
            _sorting = sorting;
        }

        public CommonResult<IQueryable<DrinksInfoDto>> GetDrinksList()
        {
            IQueryable<DrinksInfoDto> DrinksList;
            if (_db.Drinks.Any())
            {
                return CommonResult<IQueryable<DrinksInfoDto>>
                    .Failure<IQueryable<DrinksInfoDto>>("No drinks Yet...");
            }

            try
            {
                DrinksList = from a in _db.Drinks
                             join b in _db.Locations
                                 on a.LocationOfDrinkId equals b.Id
                             join c in _db.DrinkTypes
                                 on a.DrinkTypeId equals c.Id
                             select new DrinksInfoDto
                             {
                                 Name = a.Name,
                                 Price = a.Price,
                                 Type = c.Name,
                                 Location = b.Name
                             };
            }
            catch (Exception ex)
            {
                return CommonResult<IQueryable<DrinksInfoDto>>
                    .Failure<IQueryable<DrinksInfoDto>>("Something Wrong with Db" + ex.Message);
            }

            return CommonResult<IQueryable<DrinksInfoDto>>
                .Success(DrinksList);
        }

        public CommonResult<IEnumerable<LocationFeedbackDto>> GetFeedbackList(int id)
        {

            IEnumerable<LocationFeedbackDto> loc = _db.LocationFeedbacks.Where(a => a.LocationId == id).Select(a =>  new LocationFeedbackDto
            {
                Id = a.Id,
                LocationId = a.LocationId,
                Feedback = a.Feedback,
                User = a.User
            });


            return CommonResult<IEnumerable<LocationFeedbackDto>>
                .Success<IEnumerable<LocationFeedbackDto>>(loc);
            

            
        }

        public IEnumerable<string> GetNotifications(string UserId)
        {

            var result = from noti in _db.NotificationUsers
                            join a in _db.Notifications
                               on noti.NotificationId equals a.Id
                             where UserId == noti.UserAccountId
                                 where noti.IsRead == false
                                   select a.NotificationText;
            if (result.Any())
            {
                return result;
            }
            IList<string> noInformation = new List<string>
            {
              "No notifications has been added for a while"
            };
            return noInformation;
        }

        public CommonResult<IEnumerable<LocationFormDto>> ShowBarsInformaiton(string sortOrder, string searchString = null)
        {
            if (_db.Locations.Count() == 0)
            {
                return CommonResult<IEnumerable<LocationFormDto>>
                    .Failure<IEnumerable<LocationFormDto>>
                    ("The DB does not contain any location yet...");
            }

            var adapter = new SqlDataAdapter();
            var con = new SqlConnection();
            con.ConnectionString = ConnectionString;
            var select = new SqlCommand("SELECT Id, Name, Address, Latitude, Longitude" +
                                               " FROM Locations", con);

        //    select.Parameters.AddWithValue("@Name", searchString);
            adapter.SelectCommand = select;

            var ds = new DataSet();
            adapter.Fill(ds, "Locations");
            var table = ds.Tables[0];
            var data = _sorting.Mapping(table, searchString).ToList();

                       return CommonResult<IEnumerable<LocationFormDto>>
                .Success(_sorting.SortByQuery(data
                , sortOrder));
        }

        
    }
}

