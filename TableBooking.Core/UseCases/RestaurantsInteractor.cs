using Core.Contracts.DataAccess;
using Core.Entities;
using System.Collections.Generic;

namespace Core.UseCases
{
    public class RestaurantsInteractor
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsInteractor(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void AddRestaurant(RestaurantEntity restaurant)
        {
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
        }

        public IEnumerable<RestaurantEntity> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public void RemoveRestaurant(RestaurantEntity restaurant)
        {
            _restaurantRepository.Remove(restaurant);
            _restaurantRepository.SaveChanges();
        }
    }
}