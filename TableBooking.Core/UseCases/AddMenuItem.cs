using Core.Contracts.DataAccess;
using Core.Entities;

namespace Core.UseCases
{
    public class AddMenuItem
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public AddMenuItem(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Add(RestaurantEntity restaurant, MenuItemEntity menuItem)
        {
            menuItem.Restaurant = restaurant;
            _restaurantRepository.Get(restaurant.Id).MenuItems.Add(menuItem);
            _restaurantRepository.SaveChanges();
        }
    }
}