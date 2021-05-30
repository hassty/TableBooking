using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Exceptions;

namespace Core.UseCases
{
    public class AddRestaurant
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public AddRestaurant(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        /// <exception cref="ItemAlreadyExistsException"></exception>
        public void Add(RestaurantEntity restaurant)
        {
            if (_restaurantRepository.ContainsRestaurant(restaurant.Name, restaurant.Address))
            {
                throw new ItemAlreadyExistsException($"Restaurant {restaurant.Name} {restaurant.Address} already exists");
            }

            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}