using Core.Contracts.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Database
{
    public class RestaurantRepository : GenericRepository<RestaurantEntity>, IRestaurantRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public RestaurantRepository(DbContext context)
            : base(context)
        {
        }

        public bool ContainsRestaurant(string name, string address)
        {
            return _tableBookingContext.Restaurants.FirstOrDefault(r => r.Name == name && r.Address == address) != null;
        }

        public RestaurantEntity GetRestaurantByNameAndAddress(string name, string address)
        {
            return _tableBookingContext.Restaurants
                .Include(r => r.OrderOptions)
                .Include(r => r.MenuItems)
                .FirstOrDefault(r => r.Name == name && r.Address == address);
        }

        public void UpdateMenuItems(RestaurantEntity restaurant)
        {
            var parent = _tableBookingContext.Restaurants
                .Where(p => p.Id == restaurant.Id)
                .Include(p => p.MenuItems)
                .FirstOrDefault();

            parent.MenuItems = _tableBookingContext.MenuItems.Where(c => c.Id == restaurant.Id).ToList();

            _tableBookingContext.Entry(parent).State = EntityState.Modified;
        }
    }
}