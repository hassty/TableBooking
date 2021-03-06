using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases
{
    public class GetRestaurantMenuItems
    {
        private IRestaurantRepository _restaurantRepository;

        public GetRestaurantMenuItems(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<MenuItemEntity> GetMenuItems(RestaurantEntity restaurant)
        {
            return _restaurantRepository.GetRestaurantByNameAndAddress(restaurant.Name, restaurant.Address).MenuItems.ToList();
        }
    }
}